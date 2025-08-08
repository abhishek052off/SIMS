using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIMSWeb.Data.Migrations
{
    public partial class AddDbSetForNewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Courses_CourseId",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Students_StudentId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Assignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_StudentId",
                table: "Submissions",
                newName: "IX_Submissions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_AssignmentId",
                table: "Submissions",
                newName: "IX_Submissions_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignments",
                newName: "IX_Assignments_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 180, DateTimeKind.Local).AddTicks(1465));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 180, DateTimeKind.Local).AddTicks(1476));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1,
                column: "HireDate",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 180, DateTimeKind.Local).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HireDate",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 180, DateTimeKind.Local).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 179, DateTimeKind.Local).AddTicks(8017));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 179, DateTimeKind.Local).AddTicks(8028));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 179, DateTimeKind.Local).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 179, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 8, 8, 4, 43, 179, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Students_StudentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Assignment");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_StudentId",
                table: "Submission",
                newName: "IX_Submission_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submission",
                newName: "IX_Submission_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignment",
                newName: "IX_Assignment_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignment",
                table: "Assignment",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Courses_CourseId",
                table: "Assignment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Assignment_AssignmentId",
                table: "Submission",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Students_StudentId",
                table: "Submission",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
