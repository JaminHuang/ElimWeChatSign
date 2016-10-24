namespace ElimWeChatSign.Model
{
	using System.Runtime.Serialization;

	using Titan;

	/// <summary>
	///     Database Connection.
	/// </summary>
	public class DbSessionHelper
	{
		/// <summary>
		///     open database connection
		/// </summary>
		/// <returns>Database connection session</returns>
		public static IDbSession OpenSession()
		{
			var cn = DbSessionFactory.CreateAndOpenDbSessionFromConfig("ec");
			cn.Open();
			return cn;
		}
	}

	/// <summary>
	///     the EntityBase.
	/// </summary>
	[DataContract]
	public class Entity : EntityBase
	{
		/// <summary>
		/// 打开数据库会话
		/// </summary>
		/// <returns>
		/// The <see cref="IDbSession"/>.
		/// </returns>
		public override IDbSession OpenDbSession()
		{
			// throw new InvalidOperationException("Dacai.Core中的实体打开数据库时需要从外部传入IDbSession");
			return DbSessionFactory.CreateAndOpenDbSessionFromConfig("ec");
		}
	}

	/// <summary>
	/// 实体集合基类
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	[DataContract]
	public class EntityCollection<T> : EntityCollectionBase<T>
	{
		/// <summary>
		/// 打开数据库会话
		/// </summary>
		/// <returns>
		/// The <see cref="IDbSession"/>.
		/// </returns>
		public override IDbSession OpenDbSession()
		{
			// throw new InvalidOperationException("Dacai.Core中的实体打开数据库时需要从外部传入IDbSession");
			return DbSessionFactory.CreateAndOpenDbSessionFromConfig("ec");
		}
	}

	/// <summary>
	///     实体接口
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(IConditionExpression conditionExpression, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(IConditionExpression conditionExpression, params string[] updatePropertyNames);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			IDbSession session,
			IConditionExpression conditionExpression,
			params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			IDbSession session,
			IConditionExpression conditionExpression,
			params string[] updatePropertyNames);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			NameMapping tableMapping,
			IConditionExpression conditionExpression,
			params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			NameMapping tableMapping,
			IConditionExpression conditionExpression,
			params string[] updatePropertyNames);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			IDbSession session,
			NameMapping tableMapping,
			IConditionExpression conditionExpression,
			params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The batch update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="conditionExpression">
		/// The condition expression.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int BatchUpdate(
			IDbSession session,
			NameMapping tableMapping,
			IConditionExpression conditionExpression,
			params string[] updatePropertyNames);

		/// <summary>
		/// The delete.
		/// </summary>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Delete();

		/// <summary>
		/// The delete.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Delete(IDbSession session);

		/// <summary>
		/// The delete.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Delete(NameMapping tableMapping);

		/// <summary>
		/// The delete.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Delete(IDbSession session, NameMapping tableMapping);

		/// <summary>
		/// The exists.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Exists();

		/// <summary>
		/// The exists.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Exists(IDbSession session);

		/// <summary>
		/// The exists.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Exists(NameMapping tableMapping);

		/// <summary>
		/// The exists.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Exists(IDbSession session, NameMapping tableMapping);

		/// <summary>
		/// The insert.
		/// </summary>
		void Insert();

		/// <summary>
		/// The insert.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		void Insert(IDbSession session);

		/// <summary>
		/// The insert.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		void Insert(NameMapping tableMapping);

		/// <summary>
		/// The insert.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Insert(IDbSession session, NameMapping tableMapping);

		/// <summary>
		/// The select.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select();

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="queryExpression">
		/// The query expression.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(QueryExpression queryExpression);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="outputPropertyNames">
		/// The output property names.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(params string[] outputPropertyNames);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, NameMapping tableMapping);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="outputPropertyNames">
		/// The output property names.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, params string[] outputPropertyNames);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="queryExpression">
		/// The query expression.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, QueryExpression queryExpression);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(NameMapping tableMapping, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="outputPropertyNames">
		/// The output property names.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(NameMapping tableMapping, params string[] outputPropertyNames);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, NameMapping tableMapping, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The select.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="outputPropertyNames">
		/// The output property names.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool Select(IDbSession session, NameMapping tableMapping, params string[] outputPropertyNames);

		/// <summary>
		/// The update.
		/// </summary>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update();

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(NameMapping tableMapping);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(params string[] updatePropertyNames);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session, NameMapping tableMapping);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session, params string[] updatePropertyNames);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(NameMapping tableMapping, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(NameMapping tableMapping, params string[] updatePropertyNames);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="expressionBuilders">
		/// The expression builders.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session, NameMapping tableMapping, params ExpressionBuilder[] expressionBuilders);

		/// <summary>
		/// The update.
		/// </summary>
		/// <param name="session">
		/// The session.
		/// </param>
		/// <param name="tableMapping">
		/// The table mapping.
		/// </param>
		/// <param name="updatePropertyNames">
		/// The update property names.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		int Update(IDbSession session, NameMapping tableMapping, params string[] updatePropertyNames);
	}
}