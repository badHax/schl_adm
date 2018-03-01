namespace SchoolAdmin.Frontend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gpa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Weight", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "CreditsAttempted", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "CreditsEarned", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "CreditHours", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CreditHours", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "CreditsEarned");
            DropColumn("dbo.Students", "CreditsAttempted");
            DropColumn("dbo.Courses", "Weight");
        }
    }
}
