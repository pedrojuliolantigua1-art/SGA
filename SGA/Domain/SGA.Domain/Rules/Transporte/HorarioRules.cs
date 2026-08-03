using SGA.Domain.Entities.Transporte;
using SGA.Domain.Error;
using SGA.Domain.Validation;

namespace SGA.Domain.Rules
{
    public static class HorarioRules
    {
        public static Result Validar(HorarioRuta? horario, bool rutaActiva = true, int? rutaIdEsperado = null)
        {
            if (horario is null)
            {
                return Result.Fallo(DomainErrors.General.CampoRequerido("horario"));
            }

            var validacion = ValidationGeneral.IdValido(horario.RutaId, "ruta del horario");
            if (validacion.EsFallo)
            {
                return validacion;
            }

            if (horario.HoraLlegadaEstimada <= horario.HoraSalida)
            {
                return Result.Fallo(DomainErrors.CatalogoTransporte.HorarioInvalido);
            }

            if (rutaIdEsperado is not null && horario.RutaId != rutaIdEsperado)
            {
                return Result.Fallo(DomainErrors.CatalogoTransporte.HorarioNoPerteneceRuta);
            }

            if (!rutaActiva)
            {
                return Result.Fallo(DomainErrors.CatalogoTransporte.RutaInactiva);
            }

            return Result.Ok();
        }
    }
}
