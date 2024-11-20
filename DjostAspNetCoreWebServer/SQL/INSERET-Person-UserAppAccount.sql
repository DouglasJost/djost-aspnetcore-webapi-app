
/*
DELETE Person WHERE PersonId = '4EC76740-6895-40F4-ABB8-3FBAB440FFF1'

SELECT NEWID()

-- SQL FALSE : 0
-- SQL TRUE  : 1

INSERT INTO Person (PersonId, Inactive, FirstName, MiddleName, LastName, Suffix, UserDefined, LastModifiedDate)
  VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'Fn', null, 'Ln', null, 1, GETUTCDATE())
*/

/*
UPDATE Person 
  SET FirstName = 'jwt', LastName = 'Issuer' 
  WHERE PersonId = '4EC76740-6895-40F4-ABB8-3FBAB440FFF1'
*/

select * from Person 

-- ===================================

/*
DELETE UserAppAccount WHERE PersonId = '4EC76740-6895-40F4-ABB8-3FBAB440FFF1'

INSERT INTO UserAppAccount (PersonId, Inactive, Login, Password, SID, UserDefined, LastModifiedDate)
  VALUES ('4EC76740-6895-40F4-ABB8-3FBAB440FFF1', 0, 'Login', 'HelloGoodbye', null, 1, GETUTCDATE())
*/

/*
UPDATE UserAppAccount
  SET Login = 'JwtIssuer'
  WHERE PersonId = '4EC76740-6895-40F4-ABB8-3FBAB440FFF1'
*/

select * from UserAppAccount
