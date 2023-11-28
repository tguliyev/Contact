# Contact API 



Welcome to the Contact API documentation! 
## Table of Contents

- [Technical Info]
- [Authentication]
- [Contact.API]
- [Contact.MVC]
- [Error Codes]



## Technical Info

This API designed with Clean Architecture , we have multiple layers such as :
- [Domain]
- [Application]
- [Infrastructure]
- [Presentation]

Presentation consist of 2 applications , one of them is Contact.API and another one is Contact.MVC

Contact.API is communcation bridge for Contact.MVC

In my API architecture I have used Service based logic which means each Entity has service in order to use them functionalities.

- [IUserService]
- [IAccountService]
- [IUserContactService]

and some other services for Application Logic

I have generic ApiResult<TResponse> class in order to make same API response pattern in all services.

ApiResult consists of 2 main function OK and Error

- [Response]
- [StatusCode]
- [ErrorCode]
- [Description]

## Authentication

I have used JWT bearer Authentication . Each apiuser must get token from API in order to use some endpoints
When apiuser login the API , then api send token also in cookie.



## Contact.API

- [/api/account/login - POST]
- [/api/account/register -POST]
- [/api/users/contacts - GET]
- [/api/users/contacts - POST]
- [/api/users/contacts/{contactId} -GET]
- [/api/users/contacts/{contactId} - PUT]

Custom [Auth] Authorization filter attribute checks the JWT token validity

## Contact.MVC
Contact MVC consume data from Contact.API . As Authorization Contact.MVC sends token from header which key is Authorization.
- [Account/Login]
- [Account/Register]
- [Contact/List]
- [Contact/Add]
- [Contact/Edit]
- [Contact/Delete]
  
## Sharing Session

API has JWT authentication. When User tries to login to the system , API response to user via cookie . I host jwt token in cookie with SameSiteMode NONE and same domain path with other projects (ASP.NET Core MVC).


## Error Codes

 EMAIL_OR_PASSWORD_IS_NOT_CORRECT = 1_0_0,

 USER_IS_ALREADY_EXISTS_WITH_THIS_EMAIL = 1_0_1,

 USER_IS_NOT_EXISTS = 1_0_2,


 USER_CONTACT_IS_NOT_EXISTS = 2_0_0,

 USER_CONTACT_IS_ALREADY_EXISTS = 2_0_1,


 AUTH_TOKEN_IS_EMPTY = 3_0_0
