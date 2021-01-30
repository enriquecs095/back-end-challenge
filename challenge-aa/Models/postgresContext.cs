using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace challenge_aa.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ListaCarrito> ListaCarritos { get; set; }
        public virtual DbSet<Mensaje> Mensajes { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Ordene> Ordenes { get; set; }
        public virtual DbSet<OrdenesProducto> OrdenesProductos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

               IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
                optionsBuilder.UseNpgsql(configuration["ConnectionString:RDSConnection"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.Idadministrador)
                    .HasName("administrador_pkey");

                entity.ToTable("Administrador");

                entity.Property(e => e.Idadministrador)
                    .HasColumnName("idadministrador")
                    .HasDefaultValueSql("nextval('administrador_seq'::regclass)");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Administradors)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("usuarioidusuario");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(e => e.Idchat)
                    .HasName("Chat_pkey");

                entity.ToTable("Chat");

                entity.Property(e => e.Idchat)
                    .HasColumnName("idchat")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 999999L, null, null);

                entity.Property(e => e.Idadministrador).HasColumnName("idadministrador");

                entity.Property(e => e.Idcliente).HasColumnName("idcliente");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdadministradorNavigation)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.Idadministrador)
                    .HasConstraintName("administradoridadministrador");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.Idcliente)
                    .HasConstraintName("clientesidclientes");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente)
                    .HasName("Clientes_pkey");

                entity.Property(e => e.Idcliente)
                    .HasColumnName("idcliente")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("usuariosidusuarios");
            });

            modelBuilder.Entity<ListaCarrito>(entity =>
            {
                entity.HasKey(e => e.IdLista)
                    .HasName("Lista_Carrito_pkey");

                entity.ToTable("Lista_carrito");

                entity.Property(e => e.IdLista)
                    .HasColumnName("id_lista")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 999999L, null, null);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.ListaCarritos)
                    .HasForeignKey(d => d.Idproducto)
                    .HasConstraintName("idproducto");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.ListaCarritos)
                    .HasForeignKey(d => d.Idusuario)
                    .HasConstraintName("idusuario");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.HasKey(e => e.Idmensaje)
                    .HasName("mensajes_pkey");

                entity.Property(e => e.Idmensaje)
                    .HasColumnName("idmensaje")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Fechamensaje).HasColumnName("fechamensaje");

                entity.Property(e => e.Idchat).HasColumnName("idchat");

                entity.Property(e => e.Mensaje1)
                    .HasMaxLength(50)
                    .HasColumnName("mensaje");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.IdchatNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.Idchat)
                    .HasConstraintName("chatidchat");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.Idmenu)
                    .HasName("menu_pkey");

                entity.ToTable("Menu");

                entity.Property(e => e.Idmenu)
                    .HasColumnName("idmenu")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ordene>(entity =>
            {
                entity.HasKey(e => e.Idorden)
                    .HasName("ordenes_pkey");

                entity.Property(e => e.Idorden)
                    .HasColumnName("idorden")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Fechaorden).HasColumnName("fechaorden");

                entity.Property(e => e.Idcliente).HasColumnName("idcliente");

                entity.Property(e => e.Totalorden)
                    .HasColumnType("money")
                    .HasColumnName("totalorden");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Ordenes)
                    .HasForeignKey(d => d.Idcliente)
                    .HasConstraintName("clientesidcliente");
            });

            modelBuilder.Entity<OrdenesProducto>(entity =>
            {
                entity.HasKey(e => e.IdOrdenesProducto)
                    .HasName("Ordenes_productos_pkey");

                entity.ToTable("Ordenes_productos");

                entity.Property(e => e.IdOrdenesProducto)
                    .HasColumnName("id_ordenes_producto")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 999999L, null, null);

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdOrden).HasColumnName("id_orden");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.TotalProducto)
                    .HasColumnType("money")
                    .HasColumnName("total_producto");

                entity.HasOne(d => d.IdOrdenNavigation)
                    .WithMany(p => p.OrdenesProductos)
                    .HasForeignKey(d => d.IdOrden)
                    .HasConstraintName("ordenesidordenes");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.OrdenesProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("productosidproductos");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("productos_pkey");

                entity.Property(e => e.Idproducto)
                    .HasColumnName("idproducto")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(130)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Idmenu).HasColumnName("idmenu");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(27)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("precio");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("url");

                entity.HasOne(d => d.IdmenuNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.Idmenu)
                    .HasConstraintName("menuidmenu");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Idreview)
                    .HasName("reviews_pkey");

                entity.Property(e => e.Idreview)
                    .HasColumnName("idreview")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(300)
                    .HasColumnName("mensaje");

                entity.Property(e => e.Valoracion).HasColumnName("valoracion");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Idproducto)
                    .HasConstraintName("productosidproductos");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("usuarios_pkey");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 99999L, null, null);

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(30)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .HasColumnName("correo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rol).HasColumnName("rol");
            });

            modelBuilder.HasSequence("administrador_seq").HasMax(9999999);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
