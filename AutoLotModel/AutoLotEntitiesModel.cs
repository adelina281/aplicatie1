namespace AutoLotModel
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class AutoLotEntitiesModel : DbContext
	{
		public AutoLotEntitiesModel()
			: base("name=AutoLotEntitiesModel")
		{
		}

		public virtual DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.Property(e => e.Name)
				.IsFixedLength();

			modelBuilder.Entity<Product>()
				.Property(e => e.Size)
				.IsFixedLength();
		}
	}
}
