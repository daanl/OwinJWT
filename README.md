OwinJWT
=======

# Description

Authentication via JWT (Json Web Token), an example app purely owin using Microsoft.Owin.Security.Jwt middleware. 

# Demo

Starting application will launch brower and return: Not Authenticated.

You can retrieve a JWT by doing a request to: http://localhost:36215/auth, that will give back an json object contain an JsonWebToken.

Doing a request with the following Authorization header will return: Authenticated SomeUser 

```javascript
Content-Type: application/json
Authorization: Bearer <yourtokenhere>
Host: localhost:36215
```
