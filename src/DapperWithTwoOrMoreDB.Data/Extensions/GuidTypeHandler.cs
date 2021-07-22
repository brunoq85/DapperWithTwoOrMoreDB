using Dapper;
using System;
using System.Data;

namespace DapperWithTwoOrMoreDB.Data.Extensions
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            var inVal = (byte[])value; return new Guid(inVal);
        }

        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }
    }
}
