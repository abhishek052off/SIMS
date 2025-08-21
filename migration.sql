IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;


BEGIN TRANSACTION;


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);


CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [EnrollmentDate] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);


CREATE TABLE [Teachers] (
    [Id] int NOT NULL IDENTITY,
    [HireDate] datetime2 NOT NULL,
    [Department] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Teachers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);


CREATE TABLE [Courses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsCompleted] bit NULL,
    [TeacherId] int NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id])
);


CREATE TABLE [Assignments] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [MaxScore] int NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [CourseId] int NOT NULL,
    CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Assignments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);


CREATE TABLE [Enrollments] (
    [StudentId] int NOT NULL,
    [CourseId] int NOT NULL,
    [Term] int NOT NULL,
    [Marks] float NOT NULL,
    [Comments] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY ([StudentId], [CourseId]),
    CONSTRAINT [FK_Enrollments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
);


CREATE TABLE [Submissions] (
    [Id] int NOT NULL IDENTITY,
    [AssignmentId] int NOT NULL,
    [StudentId] int NOT NULL,
    [Score] float NOT NULL,
    [Feedback] nvarchar(max) NULL,
    [SubmittedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Submissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Submissions_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Submissions_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id])
);


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Email', N'Name', N'Password', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [CreatedAt], [Email], [Name], [Password], [Role])
VALUES (1, '2025-08-21T18:14:51.9232084+05:30', N'admin@gmail.com', N'Admin', N'$2a$10$Y5Kbsws5Ilj4owR3NnzSTOj8SlPblzoJCu9MGTGnPQBzW/D8xi6b6', N'Admin'),
(2, '2025-08-21T18:14:51.9911806+05:30', N'keya@gmail.com', N'Keya', N'$2a$10$ewcEyPCpVjkINaeXEWTmAuea7y1hF1h3MC6Ba48xBMcTWSbnxawAa', N'Student'),
(3, '2025-08-21T18:14:52.0591132+05:30', N'tiya@gmail.com', N'Tiya', N'$2a$10$Pn7QVmtTTsZRLKQIrVFB.eUX1WJLulJ1Z4q9N7dz8j1RHJEsbVrnW', N'Teacher'),
(4, '2025-08-21T18:14:52.1271699+05:30', N'nav@gmail.com', N'Naveen', N'$2a$10$9cWW4ulXph6W3D8nCfVwye5ftE3k0mqDmJEaKedbMbHPjcsyhdhDW', N'Teacher'),
(5, '2025-08-21T18:14:52.1956630+05:30', N'sid@gmail.com', N'Sid', N'$2a$10$EDfXbEcKSuTh8AI/WCs5wuUUCcQdEbJEA1ZULoVVGtMUDDMDwE/h.', N'Student');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Email', N'Name', N'Password', N'Role') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;


CREATE INDEX [IX_Assignments_CourseId] ON [Assignments] ([CourseId]);


CREATE INDEX [IX_Courses_TeacherId] ON [Courses] ([TeacherId]);


CREATE INDEX [IX_Enrollments_CourseId] ON [Enrollments] ([CourseId]);


CREATE UNIQUE INDEX [IX_Students_UserId] ON [Students] ([UserId]);


CREATE INDEX [IX_Submissions_AssignmentId] ON [Submissions] ([AssignmentId]);


CREATE INDEX [IX_Submissions_StudentId] ON [Submissions] ([StudentId]);


CREATE UNIQUE INDEX [IX_Teachers_UserId] ON [Teachers] ([UserId]);


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250821124452_InitialCreate', N'6.0.36');


COMMIT;


