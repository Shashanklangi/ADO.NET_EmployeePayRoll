use [EmployeeServices]

Create PROCEDURE RemoveEmployeeData  
 @ID int  
  
AS  
SET XACT_ABORT ON;  
SET NOCOUNT ON;  
BEGIN  
BEGIN TRY  
BEGIN TRANSACTION;  
-- SET NOCOUNT ON added to prevent extra result sets from  
-- interfering with SELECT statements.  
SET NOCOUNT ON;  
  
DECLARE @result bit = 0;  
DECLARE @Emp_ID int = 0;  
   
  set @Emp_ID = (select ID from EmployeePayroll where ID = @ID);  
  
IF (@Emp_ID = @ID)  
BEGIN;  
DELETE from EmployeePayroll where ID = @Emp_ID  
END  
ELSE  
 BEGIN;  
 THROW 50001, 'employee is not exist', -1;  
END  
COMMIT TRANSACTION;  
SET @result = 1;  
return @result;  
END TRY  
BEGIN CATCH  
  
IF(XACT_STATE()) = -1  
 BEGIN  
  PRINT  
   'transaction is uncommitable' + 'rolling back transaction'  
   ROLLBACK TRANSACTION;  
   return @result;  
 END;  
ELSE IF (XACT_STATE()) = 1  
 BEGIN  
  PRINT  
   'transaction is  commitable' + ' commiting back transaction'  
   COMMIT TRANSACTION;  
   SET @result =1;  
   return @result;  
 END;  
END CATCH  
END

SELECT * FROM EmployeePayroll