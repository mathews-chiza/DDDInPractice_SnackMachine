using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Linq;
using System.Reflection;

namespace DDDInPractice.Business
{
    internal static class SessionFactory
    {
        private static ISessionFactory _sessionFactory;

        internal static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public static void Init(string connectioString)
        {
            _sessionFactory = BuildSessionFactory(connectioString);
        }

        private static ISessionFactory BuildSessionFactory(string connectioString)
        {
            var configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectioString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("ID"),
                        ConventionBuilder.Property
                            .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<HiLoConvention>()
                    );

            return configuration.BuildSessionFactory();
        }
    }

    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table($"[dbo].[{instance.EntityType.Name}]");
        }
    }

    public class HiLoConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "ID");
            instance.GeneratedBy.HiLo("[dbo].[Ids]", "NextHigh", "9", $"EntityName = '{instance.EntityType.Name}'");
        }
    }
}
