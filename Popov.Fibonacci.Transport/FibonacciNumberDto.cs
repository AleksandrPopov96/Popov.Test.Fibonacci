using System.ComponentModel.DataAnnotations;

namespace Popov.Fibonacci.Abstract
{
    public record FibonacciNumberDto
    {
        /// <summary>
        /// Id расчета
        /// </summary>
        public Guid CalcId { get; init; }

        /// <summary>
        /// Индекс текущего элемента
        /// </summary>
        [Required]
        public int Index { get; init; }

        /// <summary>
        /// Значение текущего элемента
        /// </summary>
        [Required]
        public long Value { get; init; }
    }
}