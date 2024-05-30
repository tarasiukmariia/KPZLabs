using System;

// Інтерфейс для реалізацій рендерингу
public interface IRenderer
{
    void Render(string shape);
}

// Базовий клас Shape
public abstract class Shape
{
    protected IRenderer renderer;

    protected Shape(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void Draw();
}

// Клас Circle
public class Circle : Shape
{
    public Circle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Circle");
    }
}

// Клас Square
public class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Square");
    }
}

// Клас Triangle
public class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer) { }

    public override void Draw()
    {
        renderer.Render("Triangle");
    }
}

// Векторний рендерер
public class VectorRenderer : IRenderer
{
    public void Render(string shape)
    {
        Console.WriteLine($"Drawing {shape} as lines");
    }
}

// Растровий рендерер
public class RasterRenderer : IRenderer
{
    public void Render(string shape)
    {
        Console.WriteLine($"Drawing {shape} as pixels");
    }
}

public class Program
{
    public static void Main()
    {
        // Створення рендерерів
        IRenderer vectorRenderer = new VectorRenderer();
        IRenderer rasterRenderer = new RasterRenderer();

        // Створення фігур з різними рендерерами
        Shape circle = new Circle(vectorRenderer);
        Shape square = new Square(rasterRenderer);
        Shape triangle = new Triangle(vectorRenderer);

        // Малювання фігур
        circle.Draw();
        square.Draw();
        triangle.Draw();
    }
}
