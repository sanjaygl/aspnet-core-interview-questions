namespace API.Middleware
{
    // Middleware handles incoming HTTP requests and outgoing HTTP responses.
    //
    // -------------------------------------------------------------------------
    // QUESTION 1:
    // What is middleware in ASP.NET Core?
    //
    // Middleware is a component in the HTTP request pipeline that can inspect,
    // modify, or handle the request and response.
    // -------------------------------------------------------------------------

    public class CustomMiddleware
    {
        // ---------------------------------------------------------------------
        // QUESTION 2:
        // What is RequestDelegate?
        //
        // RequestDelegate represents the next component in the HTTP pipeline.
        // It accepts HttpContext and returns a Task.
        // ---------------------------------------------------------------------

        // 1. Holds a reference to the NEXT middleware in the pipeline.
        private readonly RequestDelegate _next;


        // ---------------------------------------------------------------------
        // QUESTION 3:
        // Why do we inject RequestDelegate through the constructor?
        //
        // ASP.NET Core provides the next middleware component to our middleware
        // through the constructor.
        // ---------------------------------------------------------------------

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        // ---------------------------------------------------------------------
        // QUESTION 4:
        // What is InvokeAsync()?
        //
        // InvokeAsync() is the method ASP.NET Core calls to execute this
        // middleware for an HTTP request.
        // ---------------------------------------------------------------------
        //
        // QUESTION 5:
        // What is HttpContext?
        //
        // HttpContext contains information about the current HTTP request and
        // response, including headers, query string, user, status code, etc.
        // ---------------------------------------------------------------------

        public async Task InvokeAsync(HttpContext context)
        {
            // -----------------------------------------------------------------
            // QUESTION 6:
            // Can middleware execute logic before calling _next()?
            //
            // Yes. Code before _next() executes while the request is moving
            // forward through the pipeline.
            //
            // Examples:
            // - Validate request headers
            // - Check API key
            // - Add request information
            // - Start a stopwatch
            // -----------------------------------------------------------------

            // --- LOGIC BEFORE THE NEXT MIDDLEWARE ---
            // Example: Log request, check authentication headers.


            // -----------------------------------------------------------------
            // QUESTION 7:
            // What does await _next(context) do?
            //
            // It passes the current HttpContext to the next middleware in the
            // pipeline and allows the request to continue.
            // -----------------------------------------------------------------

            await _next(context);


            // -----------------------------------------------------------------
            // QUESTION 8:
            // What happens after await _next(context)?
            //
            // Once the downstream middleware/endpoint has finished executing,
            // control returns here and the code after _next() executes.
            // -----------------------------------------------------------------

            // --- LOGIC AFTER THE NEXT MIDDLEWARE ---
            // Example:
            // - Check response status
            // - Add response headers
            // - Stop a stopwatch
            // - Perform response-related processing


            // -----------------------------------------------------------------
            // QUESTION 9:
            // What happens if we don't call _next(context)?
            //
            // The request pipeline is short-circuited.
            // The request will not proceed to the next middleware or endpoint.
            // -----------------------------------------------------------------

            // If we don't call:
            // await _next(context);
            //
            // the middleware short-circuits the request pipeline.


            // -----------------------------------------------------------------
            // QUESTION 10:
            // What is short-circuiting?
            //
            // Short-circuiting means a middleware intentionally stops the
            // request from continuing to the next middleware or endpoint.
            // -----------------------------------------------------------------

            // Example:
            //
            // if (!IsAuthorized(context))
            // {
            //     context.Response.StatusCode = 401;
            //     return;
            // }
            //
            // await _next(context);


            // -----------------------------------------------------------------
            // QUESTION 11:
            // Can middleware modify the request before _next()?
            //
            // Yes. Middleware can inspect or modify request information before
            // passing the request to the next middleware.
            // -----------------------------------------------------------------


            // -----------------------------------------------------------------
            // QUESTION 12:
            // Can middleware modify the response after _next()?
            //
            // Yes. Code after _next() executes when the downstream pipeline
            // has completed, so response information can be inspected or
            // modified where appropriate.
            // -----------------------------------------------------------------
        }
    }
}