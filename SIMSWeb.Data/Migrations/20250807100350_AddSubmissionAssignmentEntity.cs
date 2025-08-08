using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMSWeb.Data.Migrations
{
    public partial class AddSubmissionAssignmentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Courses",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submission_Assignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submission_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsCompleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCompleted",
                value: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submission",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_StudentId",
                table: "Submission",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9898));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9916));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9711));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 6, 16, 24, 23, 415, DateTimeKind.Local).AddTicks(9729));
        }
    }
}
