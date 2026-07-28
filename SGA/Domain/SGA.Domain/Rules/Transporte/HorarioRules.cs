using SGA.Domain.Entities.Transporte;
using SGA.Domain.Error;
using SGA.Domain.Validation;

namespace SGA.Domain.Rules
{
    public static class HorarioRules
    {
        public static Result Validar(HorarioRuta? horario, Ruta? ruta = null)
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

            if (horario.HoraSalida == horario.HoraLlegadaEstimada)
            {
                return Result.Fallo(DomainErrors.CatalogoTransporte.HorarioInvalido);
            }

            if (ruta is not null && horario.RutaId != ruta.Id)
            {
                return Result.Fallo(DomainErrors.CatalogoTransporte.HorarioNoPerteneceRuta);
            }

            return Result.Ok();
        }
    }
}