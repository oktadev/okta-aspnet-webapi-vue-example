namespace WeightTrackerOkta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeightMeasurements", "Name", c => c.String());
            AddColumn("dbo.WeightMeasurements", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WeightMeasurements", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.WeightMeasurements", "Description");
            DropColumn("dbo.WeightMeasurements", "MeasuredAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WeightMeasurements", "MeasuredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.WeightMeasurements", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.WeightMeasurements", "Value", c => c.Single(nullable: false));
            DropColumn("dbo.WeightMeasurements", "DateTime");
            DropColumn("dbo.WeightMeasurements", "Name");
        }
    }
}
