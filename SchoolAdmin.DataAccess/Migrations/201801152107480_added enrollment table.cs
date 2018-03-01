namespace SchoolAdmin.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedenrollmenttable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Students", newName: "AspNetUsers");
            DropForeignKey("dbo.Grades", "CourseId", "dbo.Courses");
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId });
            
            AddColumn("dbo.Courses", "Enrollment_StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "Enrollment_CourseId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Enrollment_StudentId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Enrollment_CourseId", c => c.String(maxLength: 128));
            AddColumn("dbo.Grades", "Course_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "MiddleName", c => c.String(maxLength: 20));
            CreateIndex("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            CreateIndex("dbo.Grades", "Course_Id");
            CreateIndex("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            AddForeignKey("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments", new[] { "StudentId", "CourseId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments", new[] { "StudentId", "CourseId" });
            AddForeignKey("dbo.Grades", "Course_Id", "dbo.Courses", "Id");
            DropTable("dbo.Teachers");
            DropTable("dbo.AspNetUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FirstName = c.String(maxLength: 20),
                        LastName = c.String(maxLength: 20),
                        Password = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Grades", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments");
            DropForeignKey("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" }, "dbo.Enrollments");
            DropIndex("dbo.AspNetUsers", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            DropIndex("dbo.Grades", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "Enrollment_StudentId", "Enrollment_CourseId" });
            AlterColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.Grades", "Course_Id");
            DropColumn("dbo.AspNetUsers", "Enrollment_CourseId");
            DropColumn("dbo.AspNetUsers", "Enrollment_StudentId");
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.Courses", "Enrollment_CourseId");
            DropColumn("dbo.Courses", "Enrollment_StudentId");
            DropTable("dbo.Enrollments");
            AddForeignKey("dbo.Grades", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AspNetUsers", newName: "Students");
        }
    }
}
