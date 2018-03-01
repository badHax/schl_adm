namespace SchoolAdmin.Frontend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_credit_hours_for_grades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CreditHours", c => c.Int(nullable: false));
            AddColumn("dbo.Grades", "CreditHoursEarned", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "CreditHoursEarned");
            DropColumn("dbo.Courses", "CreditHours");
        }
    }
}
