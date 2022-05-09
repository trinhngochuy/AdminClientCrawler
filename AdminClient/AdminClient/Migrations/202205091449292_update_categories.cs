namespace AdminClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_categories : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Articles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UrlSource = c.String(),
                        Title = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        CategoryId = c.String(),
                        CreatedAt = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
