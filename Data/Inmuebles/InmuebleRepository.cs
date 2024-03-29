﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;
using System.Net;

namespace NetKubernetes.Data.Inmuebles
{
	public class InmuebleRepository : IInmuebleRepository
	{

		private readonly AppDbContext _context;
		private readonly IUsuarioSesion _usuarioSesion;
		private readonly UserManager<Usuario> _userManager;

		public InmuebleRepository(AppDbContext context, IUsuarioSesion sesion, UserManager<Usuario> userManager)
		{
			_context = context;
			_usuarioSesion = sesion;
			_userManager = userManager;
		}
		public async Task CreateInmueble(Inmueble inmueble)
		{
			var usuario = await _userManager.FindByIdAsync(_usuarioSesion.ObtenerUsuarioSesion()) ??
				throw new MiddlewareException
				(
					HttpStatusCode.Unauthorized,
					new { mensaje = "El usuario no es valido para hacer esta inserción" }
				);

			if (inmueble is null)
			{
				throw new MiddlewareException
					(
						HttpStatusCode.BadRequest,
						new { mensaje = "Los datos del inmueble son incorrectos" }
					);
			}

			inmueble.FechaCreacion = DateTime.Now;
			inmueble.UsuarioId = Guid.Parse(usuario!.Id);

			await _context.Inmuebles!.AddAsync(inmueble);
		}

		public async Task DeleteInmueble(int id)
		{
			var inmueble = await _context.Inmuebles!
				.FirstOrDefaultAsync(x => x.Id == id);

			_context.Inmuebles!.Remove(inmueble!);
		}

		public async Task<IEnumerable<Inmueble>> GetAllInmuebles()
		{
			return await _context.Inmuebles!.ToListAsync();
		}

		public async Task<Inmueble> GetInmuebleById(int id)
		{
			return await _context.Inmuebles!.FirstOrDefaultAsync(x => x.Id == id)!;
		}

		public async Task<bool> SaveChanges()
		{
			return ((await _context.SaveChangesAsync()) >= 0);
		}
	}
}
