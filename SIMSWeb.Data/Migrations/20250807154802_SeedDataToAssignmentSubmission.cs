using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMSWeb.Data.Migrations
{
    public partial class SeedDataToAssignmentSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assignment",
                columns: new[] { "Id", "CourseId", "Description", "DueDate", "MaxScore", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Solve all exercises in chapter 3", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, "Algebra Homework" },
                    { 2, 11, "Complete the lab report for week 1", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Lab Report" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(2290));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(2308));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(2309));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(578));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(589));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(590));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 21, 18, 2, 525, DateTimeKind.Local).AddTicks(591));

            migrationBuilder.InsertData(
                table: "Submission",
                columns: new[] { "Id", "AssignmentId", "Feedback", "Score", "StudentId", "SubmittedAt" },
                values: new object[] { 1, 1, "Well done", 95.0, 1, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Submission",
                columns: new[] { "Id", "AssignmentId", "Feedback", "Score", "StudentId", "SubmittedAt" },
                values: new object[] { 2, 1, "", 88.0, 2, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Submission",
                columns: new[] { "Id", "AssignmentId", "Feedback", "Score", "StudentId", "SubmittedAt" },
                values: new object[] { 3, 2, "", 45.0, 1, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Submission",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Submission",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Submission",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(5249));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(5252));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(5268));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(3512));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(3525));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(3526));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(3527));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 7, 15, 33, 50, 290, DateTimeKind.Local).AddTicks(3528));
        }
    }
}
