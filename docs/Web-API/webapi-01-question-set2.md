## What is CORS and why is it important in Web APIs?
CORS stands for Cross-Origin Resource Sharing, it is a browser security mechanism that controls whether a web application from one origin can access resources from another origin. In Web APIs we configure CORS to allow trusted frontend applications to make requests to our api while restricting unauthorized cross-origin browser requests.

## Why does Postman work but the browser doesn't?
Postman can access an API even when CORS is not configured because CORS is browser security mechanism. Browsers enforce CORS for cross-origin request made by javascript, whereas postman doesn't enforce those browser restriction.

## What is AllowAnyOrigin, AllowAnyMethod, AllowAnyHeader, AllowCredentials?
1. AllowAnyOrigin() / WithOrigins() → Defines which origins are allowed to access the API. Example: WithOrigins("https://myapp.com").
2. AllowAnyHeader() / WithHeaders() → Defines which HTTP request headers the allowed origin can send. Example: WithHeaders("Authorization", "Content-Type").
3. AllowAnyMethod() / WithMethods() → Defines which HTTP methods the allowed origin can use. Example: WithMethods("GET", "POST").
4. AllowCredentials() → Allows the browser to include credentials such as cookies in cross-origin requests. Example: AllowCredentials() with WithOrigins("https://myapp.com") and client withCredentials: true.

## What is XSS (Cross-Site Scripting)?
Cross-Site scripting is web security vulnerability where attackers can inject malicious javascript into web application, and that script is executed in another user's browser. it can be use to steal sensitive information, modify page content, or perform actions on behalf of the user.

## What is CSP (Content Security Policy)?
CSP is a security mechanism that controls which resources the browser is allowed to load or execute. It helps reduce the risk of XSS and other content-injection attacks by restricting trusted sources for scripts and other resources.

## What is Cookie?
A cookie is a small piece of data stored by the browser and sent with subsequent requests to the server. It is commonly used for session management and authentication. Cookies can be secured using attributes such as HttpOnly, Secure, and SameSite.

## What is the difference between Local Storage, Session Storage, and Cookies?
Local Storage and Session Storage are browser storage mechanisms that JavaScript can access, while cookies can be automatically sent with HTTP requests. Local Storage persists until cleared, Session Storage is associated with a browser tab/session, and cookies can have an expiration time and security attributes such as HttpOnly, Secure, and SameSite.

