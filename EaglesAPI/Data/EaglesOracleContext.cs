using System;
using System.Collections.Generic;
using Eagles.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Eagles.EF.Data;

public partial class EaglesOracleContext : DbContext
{
    public EaglesOracleContext(DbContextOptions<EaglesOracleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Attr> Attrs { get; set; }

    public virtual DbSet<AttrVal> AttrVals { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryAttrVal> InventoryAttrVals { get; set; }

    public virtual DbSet<InventoryState> InventoryStates { get; set; }

    public virtual DbSet<InventoryStatus> InventoryStatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderState> OrderStates { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrdersLine> OrdersLines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttr> ProductAttrs { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("UD_SUHASB")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("ADDRESS_PK");

            entity.Property(e => e.AddressState).IsFixedLength();
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.AddressTypeId).HasName("ADDRESS_TYPE_PK");
        });

        modelBuilder.Entity<Attr>(entity =>
        {
            entity.HasKey(e => e.AttrId).HasName("ATTR_PK");

            entity.Property(e => e.AttrId).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AttrVal>(entity =>
        {
            entity.HasKey(e => e.AttrValId).HasName("ATTR_VAL_PK");

            entity.Property(e => e.AttrValId).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrValCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrValCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrValUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.AttrValUpdtId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("CUSTOMER_PK");

            entity.HasOne(d => d.CustomerGender).WithMany(p => p.Customers).HasConstraintName("CUSTOMER_FK1");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.CustomerAddressId).HasName("CUSTOMER_ADDRESS_PK");

            entity.HasOne(d => d.CustomerAddressAddress).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK2");

            entity.HasOne(d => d.CustomerAddressAddressType).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK3");

            entity.HasOne(d => d.CustomerAddressCustomer).WithMany(p => p.CustomerAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ADDRESS_FK1");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("GENDER_PK");

            entity.Property(e => e.GenderId).HasDefaultValueSql("sys_guid() ");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("INVENTORY_PK");

            entity.Property(e => e.InventoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.InventoryProduct).WithMany(p => p.Inventories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_FK1");
        });

        modelBuilder.Entity<InventoryAttrVal>(entity =>
        {
            entity.HasKey(e => e.InventoryAttrValId).HasName("INVENTORY_ATTR_VAL_PK");

            entity.Property(e => e.InventoryAttrValId).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryAttrValCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryAttrValCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryAttrValUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.InventoryAttrValUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.InventoryAttrValAttrVal).WithMany(p => p.InventoryAttrVals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_ATTR_VAL_FK3");

            entity.HasOne(d => d.InventoryAttrValInventory).WithMany(p => p.InventoryAttrVals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_ATTR_VAL_FK1");

            entity.HasOne(d => d.InventoryAttrValProductAttr).WithMany(p => p.InventoryAttrVals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_ATTR_VAL_FK2");
        });

        modelBuilder.Entity<InventoryState>(entity =>
        {
            entity.HasKey(e => e.InventoryStateId).HasName("INVENTORY_STATE_PK");

            entity.Property(e => e.InventoryStateId).HasDefaultValueSql("sys_guid() ");
            entity.Property(e => e.InventoryStateTs).HasDefaultValueSql("current_timestamp ");

            entity.HasOne(d => d.InventoryStateInventory).WithMany(p => p.InventoryStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_STATE_FK2");

            entity.HasOne(d => d.InventoryStateInventoryStatus).WithMany(p => p.InventoryStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("INVENTORY_STATE_FK1");
        });

        modelBuilder.Entity<InventoryStatus>(entity =>
        {
            entity.HasKey(e => e.InventoryStatusId).HasName("INVENTORY_STATUS_PK");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdersId).HasName("ORDERS_PK");

            entity.HasOne(d => d.OrdersCustomer).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_FK1");
        });

        modelBuilder.Entity<OrderState>(entity =>
        {
            entity.HasKey(e => e.OrderStateId).HasName("ORDER_STATE_PK");

            entity.HasOne(d => d.OrderStateOrderStatus).WithMany(p => p.OrderStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_STATE_FK2");

            entity.HasOne(d => d.OrderStateOrders).WithMany(p => p.OrderStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_STATE_FK1");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("ORDER_STATUS_PK");

            entity.HasOne(d => d.OrderStatusNextOrderStatus).WithMany(p => p.InverseOrderStatusNextOrderStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDER_STATUS_FK1");
        });

        modelBuilder.Entity<OrdersLine>(entity =>
        {
            entity.HasKey(e => e.OrdersLineId).HasName("ORDERS_LINE_PK");

            entity.HasOne(d => d.OrdersLineInventory).WithMany(p => p.OrdersLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_LINE_FK3");

            entity.HasOne(d => d.OrdersLineOrders).WithMany(p => p.OrdersLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ORDERS_LINE_FK1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRODUCT_PK");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("SYS_GUID()");
            entity.Property(e => e.ProductCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductProductStatus).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_FK1");
        });

        modelBuilder.Entity<ProductAttr>(entity =>
        {
            entity.HasKey(e => e.ProductAttrId).HasName("PRODUCT_ATTR_PK");

            entity.Property(e => e.ProductAttrId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductAttrCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductAttrCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductAttrUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductAttrUpdtId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.ProductAttrAttr).WithMany(p => p.ProductAttrs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_ATTR_FK2");

            entity.HasOne(d => d.ProductAttrProduct).WithMany(p => p.ProductAttrs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_ATTR_FK1");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PRODUCT_PRICE_PK");

            entity.HasOne(d => d.ProductPriceProduct).WithMany(p => p.ProductPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PRODUCT_PRICE_FK1");
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.ProductStatusId).HasName("PRODUCT_STATUS_PK");

            entity.Property(e => e.ProductStatusId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("SYS_GUID()");
            entity.Property(e => e.ProductStatusCrtdDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusCrtdId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusUpdtDt).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductStatusUpdtId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
