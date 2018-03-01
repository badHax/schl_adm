namespace SchoolAdmin.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseenrollmentchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments");
            DropForeignKey("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments");
            DropIndex("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            CreateTable(
                "dbo.CourseEnrollments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.CourseId });
            
            AddColumn("dbo.Courses", "CourseEnrollment_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "CourseEnrollment_CourseId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "CourseEnrollment_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "CourseEnrollment_CourseId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "CreditHours", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Courses", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" });
            CreateIndex("dbo.AspNetUsers", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" });
            AddForeignKey("dbo.Courses", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" }, "dbo.CourseEnrollments", new[] { "Id", "CourseId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" }, "dbo.CourseEnrollments", new[] { "Id", "CourseId" });
            DropColumn("dbo.Courses", "Enrollment_StudentId");
            DropColumn("dbo.Courses", "Enrollment_CourseId");
            DropColumn("dbo.AspNetUsers", "Course_Id");
            DropColumn("dbo.AspNetUsers", "Enrollment_StudentId");
            DropColumn("dbo.AspNetUsers", "Enrollment_CourseId");
            DropTable("dbo.Enrollments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId });
            
            AddColumn("dbo.AspNetUsers", "Enrollment_CourseId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Enrollment_StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Course_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "Enrollment_CourseId", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "Enrollment_StudentId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" }, "dbo.CourseEnrollments");
            DropForeignKey("dbo.Courses", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" }, "dbo.CourseEnrollments");
            DropIndex("dbo.AspNetUsers", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" });
            DropIndex("dbo.Courses", new[] { "CourseEnrollment_Id", "CourseEnrollment_CourseId" });
            AlterColumn("dbo.Courses", "CreditHours", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "CourseEnrollment_CourseId");
            DropColumn("dbo.AspNetUsers", "CourseEnrollment_Id");
            DropColumn("dbo.Courses", "CourseEnrollment_CourseId");
            DropColumn("dbo.Courses", "CourseEnrollment_Id");
            DropTable("dbo.CourseEnrollments");
            CreateIndex("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            CreateIndex("dbo.AspNetUsers", "Course_Id");
            CreateIndex("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments", new[] { "StudentId", "CourseId" });
            AddForeignKey("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments", new[] { "StudentId", "CourseId" });
            AddForeignKey("dbo.AspNetUsers", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
