using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

public class EntityBuilder<T> where T : class
{
    private T _entityToUpdate;

    public EntityBuilder()
    {
        
    }

    public EntityBuilder<T> ForEntity(T entity)
    {
        _entityToUpdate = entity;
        return this;
    }

    public EntityBuilder<T> WithProperty(Expression<Func<T, object>> propertyExpression, object value)
    {
        if (_entityToUpdate != null)
        {
            var memberExpression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)memberExpression.Member;
            propertyInfo.SetValue(_entityToUpdate, value);
        }
        return this;
    }

    public T Build()
    {
        return _entityToUpdate;
    }
}
