using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BibliotecaVirtual.Application.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Gera uma expressão dinâmica concatenando duas expressões.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressaoA">Primeira expressão.</param>
        /// <param name="expressaoB">Segunda expressão.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expressaoA, Expression<Func<T, bool>> expressaoB)
        {
            ParameterExpression p = expressaoA.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[expressaoB.Parameters[0]] = p;

            Expression body = Expression.OrElse(expressaoA.Body, visitor.Visit(expressaoB.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        /// <summary>
        /// Classe interna para adentrar uma expressão e capturar valores de forma dinâmica.
        /// </summary>
        internal class SubstExpressionVisitor : ExpressionVisitor
        {
            public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (subst.TryGetValue(node, out Expression newValue))
                {
                    return newValue;
                }
                return node;
            }
        }
    }
}
