using BackEnd.Model;
using BackEnd.Services.Interfaces;
using DAL.Intefaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Implementations
{
    public class MazoService : IMazoService
    {
        private IUnidadDeTrabajo _unidadDeTrabajo;

        private IMazoDAL mazoDAL;

        public MazoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this._unidadDeTrabajo = unidadDeTrabajo;
        }

        private MazoModel Convertir(Mazo mazo)
        {
            MazoModel entity = new MazoModel
            {
                MazoId=mazo.MazoId,
                NombreMazo=mazo.NombreMazo,
                CreadoEn=mazo.CreadoEn

            };
            return entity;
        }

        private Mazo Convertir(MazoModel mazo)
        {
            Mazo entity = new Mazo
            {
                MazoId = mazo.MazoId,
                NombreMazo = mazo.NombreMazo,
                CreadoEn = (DateTime)mazo.CreadoEn

            };
            return entity;
        }


        public bool Add(MazoModel mazo)
        {
            _unidadDeTrabajo.MazoDAL.Add(Convertir(mazo));
            return _unidadDeTrabajo.Complete();
        }


        public MazoModel Get(int id)
        {
            return Convertir(_unidadDeTrabajo.MazoDAL.Get(id));
        }

        public IEnumerable<MazoModel> Get()
        {
            var lista = _unidadDeTrabajo.MazoDAL.GetAll();
            List<MazoModel> mazo = new List<MazoModel>();
            foreach (var item in lista)
            {
                mazo.Add(Convertir(item));
            }
            return mazo;
        }

        public bool Remove(MazoModel mazo)
        {
            _unidadDeTrabajo.MazoDAL.Remove(Convertir(mazo));
            return _unidadDeTrabajo.Complete();
        }


        public bool Edit(MazoModel mazo)
        {
            try
            {
                _unidadDeTrabajo.MazoDAL.Update(Convertir(mazo));
                return _unidadDeTrabajo.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Manejo de la excepción de concurrencia
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is MazoModel)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        if (databaseValues != null)
                        {
                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // Aquí decides cómo manejar los conflictos, por ejemplo:
                                // - Mantener el valor de la base de datos
                                // - Combinar valores
                                // - Notificar al usuario
                            }

                            // Actualizar los valores originales para reflejar lo que hay en la base de datos
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }

                // Puedes reintentar guardar los cambios aquí si es apropiado
                try
                {
                    return _unidadDeTrabajo.Complete();
                }
                catch (DbUpdateConcurrencyException retryEx)
                {
                    // Si vuelve a fallar, puedes registrar el error y notificar al usuario
                    // Registrar el error (opcional)
                    // Logger.LogError(retryEx, "Error de concurrencia al intentar editar el mazo");

                    // Notificar al usuario sobre el conflicto
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                // Logger.LogError(ex, "Error al intentar editar el mazo");
                return false;
            }
        }

    }
}
