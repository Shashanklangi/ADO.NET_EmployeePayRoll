use [EmployeeServices]

alter PROCEDURE DateAndTime
@FromDate Date,
@ToDate Date

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
select EmployeePayroll.ID, Name, PhoneNumber, Address, Department, Gender, BasicPay,Deduction,TaxablePay, IncomeTax, NetPay, StartDate from
EmployeePayroll left join DepartmentTable on EmployeePayroll.ID = DepartmentTable.Id left join Payroll on EmployeePayroll.ID = Payroll.ID where StartDate between @FromDate and @ToDate

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