using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using GymManagement.Domain.common;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Common.Middleware;

    public class EventualConsistencyMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            context.Response.OnCompleted(async () =>
            {
                try
                {
                    // fetch a queue from http context or create a new queue if it doesn't exist
                    var domainEventQueue = context.Items
                        .TryGetValue("DomainEventQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents ?
                        existingDomainEvents
                        : new Queue<IDomainEvent>();

                    // process all the domain events in the queue
                    while (domainEventQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }

                    await transaction.CommitAsync();
                }
                catch(Exception)
                {
                    // notify the client that even they got a 200 response, the request was not fully processed
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            });

            await _next(context);
        }
    }
