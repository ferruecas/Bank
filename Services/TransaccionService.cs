using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

using System.Collections.Generic;
using iText.Commons.Actions.Contexts;

namespace Bank.Services
{
    public class TransaccionService : ITransaccionService
    {
        BluesoftBankDbContext context;
        public TransaccionService(BluesoftBankDbContext dbContext)
        {

            context = dbContext;

        }

        public List<Transaccione> ObtenerMovimientosRecientes(int CuentaId, int cantidad)
        {
            var transaccion = new List<Transaccione>();

            if (cantidad == 0)
            {
                transaccion = context.Transacciones
                    .Where(t => t.CuentaId == CuentaId)
                    .OrderByDescending(t => t.FechaTransaccion)
                    //.Take(cantidad)
                    .ToList();
            }else
            {
                transaccion = context.Transacciones
                    .Where(t => t.CuentaId == CuentaId)
                    .OrderByDescending(t => t.FechaTransaccion)
                    .Take(cantidad)
                    .ToList();

            }
            return transaccion;
        }

        public List<Transaccione> GenerarExtractoMensual(int cuentaId, int mes, int año)
        {
            List < Transaccione > trans = new List < Transaccione >();
            trans= context.Transacciones
            .Where(t => t.CuentaId == cuentaId &&
            //asegurarnos de que FechaTransaccion tiene un valor para acceder al mes y año
                t.FechaTransaccion.HasValue && t.FechaTransaccion.Value.Month == mes && t.FechaTransaccion.Value.Year == año)
            .OrderByDescending(t => t.FechaTransaccion)
            .ToList();
            return trans;

        }
        public List<object> ObtenerTransaccionesConCiudad()
        {
            var transaccionesConCiudad = context.Transacciones
                .Include(t => t.Ciudad)
                .Where(t =>
                    t.Monto.Value > 1000000 &&
                    t.Tipo == "Retiro" &&
                    t.Cuenta != null &&
                    t.Cuenta.CiudadId != t.CiudadId)                
                .ToList()
                .Select(t => new
                {
                    TransaccionId = t.TransaccionId,
                    Tipo = t.Tipo,
                    Monto = t.Monto.Value,
                    FechaTransaccion = t.FechaTransaccion,
                    CuentaId = t.CuentaId,
                    CiudadId = t.CiudadId,
                    CiudadNombre = t.Ciudad?.Nombre,
                    CuentaCiudadNombre = t.Cuenta?.Ciudad?.Nombre
                })
                .ToList<dynamic>();

            return transaccionesConCiudad;
        }
        public async Task RealizarConsignacion(Transaccione transaccione)
        {
            if (transaccione.Monto <= 0)
            {
                throw new ArgumentException("El monto de la consignación debe ser mayor que cero.");
            }

            var cuenta = context.Cuentas.FirstOrDefault(c => c.CuentaId == transaccione.CuentaId);

            if (cuenta == null)
            {
                throw new InvalidOperationException("Cuenta no encontrada.");
            }


            var ciudad = context.Ciudads.FirstOrDefault(c => c.CiudadId == transaccione.CiudadId);

            if (ciudad == null)
            {
                throw new InvalidOperationException("Ciudad no encontrada.");
            }

            // Actualizar saldo de la cuenta
            cuenta.Saldo = cuenta.Saldo + transaccione.Monto;

            // Crear la transacción
            var transaccion = new Transaccione
            {
                Tipo = "Consignacion",
                Monto = transaccione.Monto,
                FechaTransaccion = DateTime.Now,
                CuentaId = transaccione.CuentaId,
                CiudadId = transaccione.CiudadId
            };

            // Agregar la transacción al contexto
            context.Transacciones.Add(transaccion);

            // Guardar los cambios en la base de datos
            context.SaveChanges();
            await context.SaveChangesAsync();
        }

        public async Task RealizarRetiro(Transaccione transaccione)
        {
            if (transaccione.Monto <= 0)
            {
                throw new ArgumentException("El monto del retiro debe ser mayor que cero.");
            }

            var cuenta = context.Cuentas.FirstOrDefault(c => c.CuentaId == transaccione.CuentaId);

            if (cuenta == null)
            {
                throw new InvalidOperationException("Cuenta no encontrada.");
            }

            if (cuenta.Saldo < transaccione.Monto)
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar el retiro.");
            }


            var ciudad = context.Ciudads.FirstOrDefault(c => c.CiudadId == transaccione.CiudadId);

            if (ciudad == null)
            {
                throw new InvalidOperationException("Ciudad no encontrada.");
            }

            cuenta.Saldo -= transaccione.Monto;

            var transaccion = new Transaccione
            {
                Tipo = "Retiro",
                Monto = transaccione.Monto,
                FechaTransaccion = DateTime.Now,
                CuentaId = transaccione.CuentaId,
                CiudadId = transaccione.CiudadId
            };

            context.Transacciones.Add(transaccion);
            await context.SaveChangesAsync();
        }

    }
    public interface ITransaccionService
    {
        public List<object> ObtenerTransaccionesConCiudad();
        public List<Transaccione> ObtenerMovimientosRecientes(int CuentaId, int cantidad);
        public List<Transaccione> GenerarExtractoMensual(int cuentaId, int mes, int año);
        public Task RealizarConsignacion(Transaccione transaccione);
        public Task RealizarRetiro(Transaccione transaccione);


    }

}

