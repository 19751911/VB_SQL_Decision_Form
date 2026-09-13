-- إنشاء قاعدة البيانات
CREATE DATABASE DecisionsDB;
GO

USE DecisionsDB;
GO

-- إنشاء جدول القرارات
CREATE TABLE Decisions (
    DecisionID INT PRIMARY KEY IDENTITY(1,1),
    DecisionNumber NVARCHAR(50) NOT NULL,
    DecisionDate DATE NOT NULL,
    Year INT NOT NULL,
    Status NVARCHAR(100),
    Chapter NVARCHAR(100),
    Description NVARCHAR(500),
    BeneficiaryName NVARCHAR(200) NOT NULL,
    Details NVARCHAR(500),
    Amount DECIMAL(10, 2),
    Notes NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO

-- إدراج بيانات عينة
INSERT INTO Decisions VALUES 
('2016/46', '2016-03-16', 2016, 'مكانة', 'الباب الثاني', 'تصحيح كراسات الإجابة', 'انسحاب يوسف الدواجي', 'حسب القرار', 410, 'ملاحظة 1', GETDATE()),
('2016/47', '2016-03-17', 2016, 'مكانة', 'الباب الثاني', 'تصحيح كراسات الإجابة', 'انسحاب علي محمد', 'حسب القرار', 450, 'ملاحظة 2', GETDATE()),
('2016/48', '2016-03-18', 2016, 'مكانة', 'الباب الثالث', 'تعديل السجلات', 'انسحاب فاطمة أحمد', 'حسب القرار', 500, 'ملاحظة 3', GETDATE()),
('2016/49', '2016-03-19', 2016, 'مكانة', 'الباب الثاني', 'تصحيح كراسات الإجابة', 'انسحاب يوسف الدواجي', 'حسب القرار', 420, 'ملاحظة 4', GETDATE());
GO

-- إنشاء مجموعة من الإجراءات المخزنة

-- إجراء البحث بالاسم
CREATE PROCEDURE sp_SearchByName
    @BeneficiaryName NVARCHAR(200)
AS
BEGIN
    SELECT DecisionID, DecisionNumber, DecisionDate, Year, Status, Chapter, 
           Description, BeneficiaryName, Details, Amount, Notes
    FROM Decisions
    WHERE BeneficiaryName LIKE '%' + @BeneficiaryName + '%'
    ORDER BY DecisionDate DESC;
END;
GO

-- إجراء الحصول على جميع الأسماء المميزة
CREATE PROCEDURE sp_GetDistinctNames
AS
BEGIN
    SELECT DISTINCT BeneficiaryName
    FROM Decisions
    ORDER BY BeneficiaryName;
END;
GO

-- إجراء الحصول على جميع السجلات لاسم معين
CREATE PROCEDURE sp_GetRecordsByName
    @BeneficiaryName NVARCHAR(200)
AS
BEGIN
    SELECT DecisionID, DecisionNumber, DecisionDate, Year, Status, Chapter, 
           Description, BeneficiaryName, Details, Amount, Notes
    FROM Decisions
    WHERE BeneficiaryName = @BeneficiaryName
    ORDER BY DecisionDate DESC;
END;
GO

-- إجراء إدراج سجل جديد
CREATE PROCEDURE sp_InsertDecision
    @DecisionNumber NVARCHAR(50),
    @DecisionDate DATE,
    @Year INT,
    @Status NVARCHAR(100),
    @Chapter NVARCHAR(100),
    @Description NVARCHAR(500),
    @BeneficiaryName NVARCHAR(200),
    @Details NVARCHAR(500),
    @Amount DECIMAL(10, 2),
    @Notes NVARCHAR(500)
AS
BEGIN
    INSERT INTO Decisions VALUES 
    (@DecisionNumber, @DecisionDate, @Year, @Status, @Chapter, @Description, 
     @BeneficiaryName, @Details, @Amount, @Notes, GETDATE());
END;
GO

-- إجراء تحديث السجل
CREATE PROCEDURE sp_UpdateDecision
    @DecisionID INT,
    @DecisionNumber NVARCHAR(50),
    @DecisionDate DATE,
    @Year INT,
    @Status NVARCHAR(100),
    @Chapter NVARCHAR(100),
    @Description NVARCHAR(500),
    @BeneficiaryName NVARCHAR(200),
    @Details NVARCHAR(500),
    @Amount DECIMAL(10, 2),
    @Notes NVARCHAR(500)
AS
BEGIN
    UPDATE Decisions
    SET DecisionNumber = @DecisionNumber, DecisionDate = @DecisionDate, Year = @Year,
        Status = @Status, Chapter = @Chapter, Description = @Description,
        BeneficiaryName = @BeneficiaryName, Details = @Details, Amount = @Amount, Notes = @Notes
    WHERE DecisionID = @DecisionID;
END;
GO

-- إجراء حذف السجل
CREATE PROCEDURE sp_DeleteDecision
    @DecisionID INT
AS
BEGIN
    DELETE FROM Decisions WHERE DecisionID = @DecisionID;
END;
GO
