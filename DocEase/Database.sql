-- Doctor Registration
CREATE TABLE Doctors (
    DoctorId INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Gender NVARCHAR(10),
    DateOfBirth DATE,
    Phone NVARCHAR(20),
    Email NVARCHAR(150) UNIQUE,
    RegistrationDate DATETIME DEFAULT GETDATE()
);

-- Doctor Profile (specialization, experience, etc.)
CREATE TABLE DoctorProfiles (
    ProfileId INT IDENTITY PRIMARY KEY,
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    Specialization NVARCHAR(100),
    Qualifications NVARCHAR(200),
    ExperienceYears INT,
    Bio NVARCHAR(MAX)
);

-- Patient Registration
CREATE TABLE Patients (
    PatientId INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Gender NVARCHAR(10),
    DateOfBirth DATE,
    Phone NVARCHAR(20),
    Email NVARCHAR(150) UNIQUE,
    RegistrationDate DATETIME DEFAULT GETDATE()
);

-- Patient Disease History
CREATE TABLE PatientDiseaseHistory (
    HistoryId INT IDENTITY PRIMARY KEY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DiseaseName NVARCHAR(200),
    DiagnosisDate DATE,
    Notes NVARCHAR(MAX)
);

-- Doctor Schedule
CREATE TABLE DoctorSchedules (
    ScheduleId INT IDENTITY PRIMARY KEY,
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    AvailableDate DATE,
    StartTime TIME,
    EndTime TIME,
    IsBooked BIT DEFAULT 0
);

-- Patient Checkup Appointments
CREATE TABLE Appointments (
    AppointmentId INT IDENTITY PRIMARY KEY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    ScheduleId INT FOREIGN KEY REFERENCES DoctorSchedules(ScheduleId),
    AppointmentDate DATETIME,
    Status NVARCHAR(50) DEFAULT 'Scheduled'
);

-- Medical Records (prescriptions, lab reports)
CREATE TABLE MedicalRecords (
    RecordId INT IDENTITY PRIMARY KEY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    RecordType NVARCHAR(50), -- Prescription, LabReport, etc.
    Description NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Prescriptions
CREATE TABLE Prescriptions (
    PrescriptionId INT IDENTITY PRIMARY KEY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    Medication NVARCHAR(200),
    Dosage NVARCHAR(100),
    Duration NVARCHAR(100),
    Notes NVARCHAR(MAX),
    DateIssued DATETIME DEFAULT GETDATE()
);

-- Feedback (patients can rate doctors)
CREATE TABLE Feedback (
    FeedbackId INT IDENTITY PRIMARY KEY,
    PatientId INT FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comments NVARCHAR(MAX),
    FeedbackDate DATETIME DEFAULT GETDATE()
);

-- Notifications (reminders for appointments, prescriptions)
CREATE TABLE Notifications (
    NotificationId INT IDENTITY PRIMARY KEY,
    UserType NVARCHAR(20), -- Doctor/Patient
    UserId INT,
    Message NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0
);

CREATE TABLE ErrorLogs (
    Id INT IDENTITY PRIMARY KEY,
    Message NVARCHAR(MAX),
    TimeStamp DATETIME NOT NULL,
    Exception NVARCHAR(MAX) NULL,
    Controller NVARCHAR(100) NULL,
    Action NVARCHAR(100) NULL,
    UserId NVARCHAR(100) NULL
);

CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    UserName NVARCHAR(100) UNIQUE NOT NULL,   -- login name
    Email NVARCHAR(150) UNIQUE NOT NULL,      -- optional, can be used for login
    PasswordHash NVARCHAR(200) NOT NULL,      -- store hashed password
    Role NVARCHAR(50) NOT NULL,               -- Doctor, Patient, Admin
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME NULL
);

ALTER TABLE Doctors
ADD UserId INT FOREIGN KEY REFERENCES Users(UserId);

ALTER TABLE Patients
ADD UserId INT FOREIGN KEY REFERENCES Users(UserId);



CREATE TABLE Payments (
    PaymentId INT IDENTITY PRIMARY KEY,
    PatientId INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientId),
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    Method NVARCHAR(50) NOT NULL,        -- Cash, Card, UPI, NetBanking
    Status NVARCHAR(50) NOT NULL,        -- Pending, Completed, Failed
    UTR NVARCHAR(100) UNIQUE NULL        -- Unique Transaction Reference (for online/UPI)
);

CREATE TABLE Insurance (
    InsuranceId INT IDENTITY PRIMARY KEY,
    PatientId INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientId),
    Provider NVARCHAR(200) NOT NULL,
    PolicyNumber NVARCHAR(100) UNIQUE NOT NULL,
    CoverageDetails NVARCHAR(MAX),
    ValidTill DATE NOT NULL
);

CREATE TABLE VideoConsultations (
    ConsultationId INT IDENTITY PRIMARY KEY,
    AppointmentId INT NOT NULL FOREIGN KEY REFERENCES Appointments(AppointmentId),
    MeetingLink NVARCHAR(300) NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Scheduled' -- Scheduled, Completed, Cancelled
);




declare @TableName sysname = 'ErrorLogs'
declare @Result varchar(max) = 'public class ' + @TableName + '
{'

select @Result = @Result + '
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }
'
from
(
    select 
        replace(col.name, ' ', '_') ColumnName,
        column_id ColumnId,
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'string'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'double'
            when 'image' then 'byte[]'
            when 'int' then 'int'
            when 'money' then 'decimal'
            when 'nchar' then 'string'
            when 'ntext' then 'string'
            when 'numeric' then 'decimal'
            when 'nvarchar' then 'string'
            when 'real' then 'float'
            when 'smalldatetime' then 'DateTime'
            when 'smallint' then 'short'
            when 'smallmoney' then 'decimal'
            when 'text' then 'string'
            when 'time' then 'TimeSpan'
            when 'timestamp' then 'long'
            when 'tinyint' then 'byte'
            when 'uniqueidentifier' then 'Guid'
            when 'varbinary' then 'byte[]'
            when 'varchar' then 'string'
            else 'UNKNOWN_' + typ.name
        end ColumnType,
        case 
            when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '?' 
            else '' 
        end NullableSign
    from sys.columns col
        join sys.types typ on
            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id
    where object_id = object_id(@TableName)
) t
order by ColumnId

set @Result = @Result  + '
}'

print @Result
