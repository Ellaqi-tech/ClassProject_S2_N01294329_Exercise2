namespace PetGrooming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroomBookings", "PetID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroomBookings", "PetID");
        }
    }
}
