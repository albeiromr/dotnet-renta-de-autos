using Dapper;
using System.Data;

namespace Infrastructure.TypeHandlers;

// este typehandler se usa por que postgres no puede leer ni parsear el
// tipo de dato DateOnly
internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    // transforma el datetime a un tipo DateTime que si pueda ser leido por
    // postgress
    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value;
    }
}
