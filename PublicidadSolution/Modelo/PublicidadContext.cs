namespace Modelo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PublicidadContext : DbContext
    {
        public PublicidadContext()
            : base("name=PublicidadContext")
        {
        }

        public virtual DbSet<CAMPANIA> CAMPANIAs { get; set; }
        public virtual DbSet<DETALLE_CAMPANIA> DETALLE_CAMPANIA { get; set; }
        public virtual DbSet<DETALLE_FACTURA> DETALLE_FACTURA { get; set; }
        public virtual DbSet<ELEMENTO> ELEMENTOes { get; set; }
        public virtual DbSet<EMPRESA> EMPRESAs { get; set; }
        public virtual DbSet<ESTADISTICA_CAMPANIA> ESTADISTICA_CAMPANIA { get; set; }
        public virtual DbSet<FACTURA_EMPRESA> FACTURA_EMPRESA { get; set; }
        public virtual DbSet<HISTORIAL_CAMPANIA> HISTORIAL_CAMPANIA { get; set; }
        public virtual DbSet<SEGMENTO_DETALLE_CAMPANIA> SEGMENTO_DETALLE_CAMPANIA { get; set; }
        public virtual DbSet<TARGET_EDAD> TARGET_EDAD { get; set; }
        public virtual DbSet<USUARIO> USUARIOs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CAMPANIA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<CAMPANIA>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<CAMPANIA>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<CAMPANIA>()
                .Property(e => e.FECHA_CREACION)
                .IsFixedLength();

            modelBuilder.Entity<CAMPANIA>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<CAMPANIA>()
                .HasMany(e => e.DETALLE_CAMPANIA)
                .WithRequired(e => e.CAMPANIA)
                .HasForeignKey(e => new { e.SEC_CAMPANIA, e.RUC })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAMPANIA>()
                .HasOptional(e => e.ESTADISTICA_CAMPANIA)
                .WithRequired(e => e.CAMPANIA);

            modelBuilder.Entity<CAMPANIA>()
                .HasMany(e => e.HISTORIAL_CAMPANIA)
                .WithOptional(e => e.CAMPANIA)
                .HasForeignKey(e => new { e.SEC_CAMPANIA, e.RUC });

            modelBuilder.Entity<DETALLE_CAMPANIA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_CAMPANIA>()
                .Property(e => e.MODO_FACTURACION)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_CAMPANIA>()
                .HasOptional(e => e.SEGMENTO_DETALLE_CAMPANIA)
                .WithRequired(e => e.DETALLE_CAMPANIA);

            modelBuilder.Entity<DETALLE_FACTURA>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_FACTURA>()
                .Property(e => e.VALOR_UNITARIO)
                .HasPrecision(5, 2);

            modelBuilder.Entity<DETALLE_FACTURA>()
                .Property(e => e.VALOR_TOTAL)
                .HasPrecision(5, 2);

            modelBuilder.Entity<DETALLE_FACTURA>()
                .Property(e => e.VALOR_DESCUENTO)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ELEMENTO>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<ELEMENTO>()
                .Property(e => e.POSICION)
                .IsUnicode(false);

            modelBuilder.Entity<ELEMENTO>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<ELEMENTO>()
                .Property(e => e.PATH)
                .IsUnicode(false);

            modelBuilder.Entity<ELEMENTO>()
                .HasMany(e => e.DETALLE_CAMPANIA)
                .WithRequired(e => e.ELEMENTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.RAZON_SOCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .Property(e => e.REPRESENTANTE)
                .IsUnicode(false);

            modelBuilder.Entity<EMPRESA>()
                .HasMany(e => e.CAMPANIAs)
                .WithRequired(e => e.EMPRESA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ESTADISTICA_CAMPANIA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.FECHA_EMISION)
                .IsFixedLength();

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.SECUENCIAL)
                .IsUnicode(false);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.VALOR_TOTAL)
                .HasPrecision(5, 2);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.PORCENTAJE_IVA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .Property(e => e.SUBTOTAL)
                .HasPrecision(5, 2);

            modelBuilder.Entity<FACTURA_EMPRESA>()
                .HasOptional(e => e.DETALLE_FACTURA)
                .WithRequired(e => e.FACTURA_EMPRESA);

            modelBuilder.Entity<HISTORIAL_CAMPANIA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORIAL_CAMPANIA>()
                .Property(e => e.FECHA_COMPRA)
                .IsFixedLength();

            modelBuilder.Entity<HISTORIAL_CAMPANIA>()
                .Property(e => e.COSTO_CLIC)
                .HasPrecision(5, 2);

            modelBuilder.Entity<HISTORIAL_CAMPANIA>()
                .Property(e => e.COSTO_DESPLIEGUE)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SEGMENTO_DETALLE_CAMPANIA>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<SEGMENTO_DETALLE_CAMPANIA>()
                .Property(e => e.HORA_INICIO)
                .IsUnicode(false);

            modelBuilder.Entity<SEGMENTO_DETALLE_CAMPANIA>()
                .Property(e => e.HORA_FIN)
                .IsUnicode(false);

            modelBuilder.Entity<SEGMENTO_DETALLE_CAMPANIA>()
                .Property(e => e.MAXIMO_HORA)
                .IsUnicode(false);

            modelBuilder.Entity<SEGMENTO_DETALLE_CAMPANIA>()
                .Property(e => e.MINIMO_HORA)
                .IsUnicode(false);

            modelBuilder.Entity<TARGET_EDAD>()
                .Property(e => e.RUC)
                .IsUnicode(false);

            modelBuilder.Entity<TARGET_EDAD>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<TARGET_EDAD>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<TARGET_EDAD>()
                .Property(e => e.GENERO)
                .IsUnicode(false);

            modelBuilder.Entity<TARGET_EDAD>()
                .HasMany(e => e.SEGMENTO_DETALLE_CAMPANIA)
                .WithRequired(e => e.TARGET_EDAD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOMBRES)
                .IsUnicode(false);
        }
    }
}
