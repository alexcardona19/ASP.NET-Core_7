﻿using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data
{
	public class LoadDataBase
	{
		public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager) {

			if (!usuarioManager.Users.Any())
			{
				var usuario = new Usuario
				{

					Nombre = "Vaxi",
					Apellido = "Drez",
					Email = "email@gmail.com",
					UserName = "vaxi.drez",
					Telefono = "943434"
				};

				await usuarioManager.CreateAsync(usuario,"PasswordVxidrez2024$");
			}

			if (!context.Inmuebles!.Any()) {
				context.Inmuebles!.AddRange(
					new Inmueble {
					  Nombre = "Casa de Playa",
					  Direccion = "Av. El sol 32",
					  Precio = 4500M,
					  FechaCreacion = DateTime.Now
					},
					new Inmueble
					{
						Nombre = "Casa de Invierno",
						Direccion = "Av. la Roca 101",
						Precio = 4500M,
						FechaCreacion = DateTime.Now
					}
				);
			}

			context.SaveChanges();
		}
	}
}
