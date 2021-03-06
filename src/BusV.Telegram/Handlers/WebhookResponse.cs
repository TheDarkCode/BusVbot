﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Telegram.Bot.Framework.Abstractions;

namespace BusV.Telegram.Handlers
{
    /// <summary>
    /// If a request exists, responds to the HTTP webhook request with a Bot API request.
    /// If not a webhook update, it makes the request explicitly.
    /// </summary>
    public class WebhookResponse : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            await next(context, cancellationToken).ConfigureAwait(false);

            if (!context.Items.ContainsKey(nameof(WebhookResponse)))
                return;

            dynamic request = context.Items[nameof(WebhookResponse)];

            if (context.IsWebhook())
            {
                var httpContext = (HttpContext) context.Items[nameof(HttpContext)];

                if (httpContext.Response.HasStarted)
                {
                    // ToDo use logger and ignore
                    throw new InvalidOperationException("");
                }

                string method = request.MethodName;
                HttpContent httpContent = request.ToHttpContent();

                string json = await httpContent.ReadAsStringAsync()
                    .ConfigureAwait(false);
                json = json.Insert(json.Length - 1, $@",""method"":""{method}""");

                httpContext.Response.StatusCode = 201;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(json, Encoding.UTF8, cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                await context.Bot.Client.MakeRequestAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
