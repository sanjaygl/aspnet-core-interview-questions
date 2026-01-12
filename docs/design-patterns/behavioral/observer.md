# Observer Pattern

## Pattern Category
**Behavioral Pattern**

## Academic Foundation

### Historical Context
The Observer pattern was formalized by the Gang of Four as part of their behavioral patterns catalog. It has roots in the Model-View-Controller (MVC) architectural pattern and event-driven programming paradigms. The pattern emerged from the need to maintain consistency between related objects without making them tightly coupled.

### Theoretical Basis
The Observer pattern is grounded in several fundamental computer science principles:

1. **Publish-Subscribe Model**: Based on the publisher-subscriber communication pattern
2. **Loose Coupling**: Subjects and observers interact through well-defined interfaces
3. **Event-Driven Architecture**: Supports reactive programming and event-driven systems
4. **Separation of Concerns**: Separates the subject's core functionality from notification logic

### Academic Classification
- **Pattern Type**: Object Behavioral
- **Scope**: Object-level (deals with object relationships and communication)
- **Intent**: Define one-to-many dependency between objects

## Intent / Definition
Define a one-to-many dependency so that when one object changes state, all its dependents are notified and updated automatically.

### Formal Definition
*"Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically."* - Gang of Four

### Academic Problem Statement
In object-oriented systems, there are scenarios where:
- Multiple objects need to be notified when another object's state changes
- The number and types of dependent objects may vary at runtime
- Objects should remain loosely coupled while maintaining consistency
- A change in one object requires updates to an unknown number of other objects

## Problem Statement
- You need to notify multiple objects about state changes
- You want to decouple subject and observers
- The number of observers may change at runtime
- You want to support broadcast communication

## Academic Analysis

### Forces and Constraints
The Observer pattern addresses several competing forces:

1. **Consistency vs. Performance**: Need for consistent state vs. notification overhead
2. **Coupling vs. Communication**: Loose coupling vs. effective communication
3. **Flexibility vs. Complexity**: Runtime flexibility vs. system complexity
4. **Push vs. Pull**: Data pushing vs. observer pulling information

### Theoretical Participants

#### **Subject (Observable)**
- **Role**: Maintains list of observers and notifies them of state changes
- **Academic Purpose**: Provides the interface for attaching and detaching observers
- **Responsibilities**: State management and observer notification

#### **Observer**
- **Role**: Defines updating interface for objects that should be notified
- **Academic Purpose**: Establishes the contract for receiving notifications
- **Design Principle**: Interface Segregation - observers only depend on notification interface

#### **ConcreteSubject**
- **Role**: Stores state of interest and sends notification when state changes
- **Academic Purpose**: Implements the observable behavior for specific domain objects
- **Responsibilities**: State storage and change detection

#### **ConcreteObserver**
- **Role**: Implements the Observer updating interface
- **Academic Purpose**: Provides specific response to state changes
- **Design Principle**: Single Responsibility - each observer handles one type of response

### Collaboration Patterns
The academic collaboration model follows these patterns:

1. **Registration**: Observers register with subjects for notifications
2. **Notification**: Subject notifies all registered observers when state changes
3. **Update**: Observers retrieve new state and update themselves accordingly
4. **Deregistration**: Observers can unregister to stop receiving notifications

## When to Use / When NOT to Use

### Academic Criteria for Application
✅ **Use Observer when:**
- **One-to-Many Dependencies**: One object's change affects multiple objects
- **Broadcast Communication**: Need to notify unknown number of objects
- **Loose Coupling Required**: Subject and observers should remain independent
- **Dynamic Relationships**: Observer relationships change at runtime
- **Event-Driven Systems**: Building reactive or event-driven architectures

❌ **Don't use Observer when:**
- **Simple Notifications**: Only one object needs notification
- **Tight Coupling Acceptable**: Direct method calls are sufficient
- **Performance Critical**: Notification overhead is unacceptable
- **Complex Dependencies**: Observers have complex interdependencies

### Academic Decision Framework
Consider these theoretical aspects:

1. **Dependency Analysis**: Is there a true one-to-many dependency?
2. **Coupling Analysis**: Will observer pattern reduce coupling?
3. **Performance Analysis**: Is notification overhead acceptable?
4. **Complexity Analysis**: Does the flexibility justify the complexity?

## UML-style Explanation (Text)

### Academic UML Structure
```
Subject Interface
├── ConcreteSubject (implements Subject, maintains observer list)
└── Observer Interface
    ├── ConcreteObserverA (implements Observer)
    └── ConcreteObserverB (implements Observer)
```

### Structural Relationships
- **Aggregation**: Subject aggregates multiple observers
- **Dependency**: Observers depend on subject for notifications
- **Association**: Bidirectional association between subject and observers

### Academic Sequence Analysis
1. **Observer Registration**: Observer calls subject.Attach(observer)
2. **State Change**: Subject's state is modified
3. **Notification Trigger**: Subject calls Notify() on all observers
4. **Observer Update**: Each observer's Update() method is called
5. **State Synchronization**: Observers retrieve and process new state

## C# Implementation Example

### Academic Implementation with Comprehensive Analysis
```csharp
/// <summary>
/// Academic Implementation: Stock Market Monitoring System
/// 
/// Theoretical Design:
/// - Subject maintains observer list and handles notifications
/// - Observers implement update interface for receiving notifications
/// - Loose coupling through interface-based design
/// - Support for dynamic observer registration/deregistration
/// </summary>

// Observer interface - defines the update contract
public interface IStockObserver
{
    void Update(Stock stock, decimal oldPrice, decimal newPrice);
}

// Subject interface - defines the observable contract
public interface IStockSubject
{
    void Attach(IStockObserver observer);
    void Detach(IStockObserver observer);
    void Notify();
}

// ConcreteSubject - implements observable behavior
public class Stock : IStockSubject
{
    private readonly List<IStockObserver> _observers;
    private readonly string _symbol;
    private decimal _price;
    
    public Stock(string symbol, decimal initialPrice)
    {
        _symbol = symbol;
        _price = initialPrice;
        _observers = new List<IStockObserver>();
    }
    
    public string Symbol => _symbol;
    public decimal Price 
    { 
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldPrice = _price;
                _price = value;
                Notify(oldPrice, value);
            }
        }
    }
    
    public void Attach(IStockObserver observer)
    {
        if (observer != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }
    
    public void Detach(IStockObserver observer)
    {
        _observers.Remove(observer);
    }
    
    public void Notify()
    {
        Notify(_price, _price);
    }
    
    private void Notify(decimal oldPrice, decimal newPrice)
    {
        // Create a copy to avoid modification during iteration
        var observersCopy = new List<IStockObserver>(_observers);
        
        foreach (var observer in observersCopy)
        {
            try
            {
                observer.Update(this, oldPrice, newPrice);
            }
            catch (Exception ex)
            {
                // Log exception but continue notifying other observers
                Console.WriteLine($"Error notifying observer: {ex.Message}");
            }
        }
    }
}

// ConcreteObserver - Portfolio tracker
public class PortfolioTracker : IStockObserver
{
    private readonly string _portfolioName;
    private readonly Dictionary<string, int> _holdings;
    
    public PortfolioTracker(string portfolioName)
    {
        _portfolioName = portfolioName;
        _holdings = new Dictionary<string, int>();
    }
    
    public void AddHolding(string symbol, int shares)
    {
        _holdings[symbol] = _holdings.GetValueOrDefault(symbol, 0) + shares;
    }
    
    public void Update(Stock stock, decimal oldPrice, decimal newPrice)
    {
        if (_holdings.TryGetValue(stock.Symbol, out int shares))
        {
            var oldValue = oldPrice * shares;
            var newValue = newPrice * shares;
            var change = newValue - oldValue;
            
            Console.WriteLine($"Portfolio '{_portfolioName}': {stock.Symbol} " +
                            $"changed from ${oldPrice:F2} to ${newPrice:F2}. " +
                            $"Portfolio impact: ${change:F2}");
        }
    }
}

// ConcreteObserver - Price alert system
public class PriceAlertSystem : IStockObserver
{
    private readonly Dictionary<string, (decimal LowThreshold, decimal HighThreshold)> _alerts;
    
    public PriceAlertSystem()
    {
        _alerts = new Dictionary<string, (decimal, decimal)>();
    }
    
    public void SetAlert(string symbol, decimal lowThreshold, decimal highThreshold)
    {
        _alerts[symbol] = (lowThreshold, highThreshold);
    }
    
    public void Update(Stock stock, decimal oldPrice, decimal newPrice)
    {
        if (_alerts.TryGetValue(stock.Symbol, out var thresholds))
        {
            if (newPrice <= thresholds.LowThreshold)
            {
                Console.WriteLine($"ALERT: {stock.Symbol} dropped to ${newPrice:F2} " +
                                $"(below threshold ${thresholds.LowThreshold:F2})");
            }
            else if (newPrice >= thresholds.HighThreshold)
            {
                Console.WriteLine($"ALERT: {stock.Symbol} rose to ${newPrice:F2} " +
                                $"(above threshold ${thresholds.HighThreshold:F2})");
            }
        }
    }
}

// ConcreteObserver - Trading bot
public class TradingBot : IStockObserver
{
    private readonly string _botName;
    private readonly decimal _buyThreshold;
    private readonly decimal _sellThreshold;
    
    public TradingBot(string botName, decimal buyThreshold, decimal sellThreshold)
    {
        _botName = botName;
        _buyThreshold = buyThreshold;
        _sellThreshold = sellThreshold;
    }
    
    public void Update(Stock stock, decimal oldPrice, decimal newPrice)
    {
        var changePercent = ((newPrice - oldPrice) / oldPrice) * 100;
        
        if (changePercent <= -_buyThreshold)
        {
            Console.WriteLine($"Bot '{_botName}': BUY signal for {stock.Symbol} " +
                            $"at ${newPrice:F2} (dropped {Math.Abs(changePercent):F1}%)");
        }
        else if (changePercent >= _sellThreshold)
        {
            Console.WriteLine($"Bot '{_botName}': SELL signal for {stock.Symbol} " +
                            $"at ${newPrice:F2} (rose {changePercent:F1}%)");
        }
    }
}
```

### Modern C# Implementation with Events
```csharp
/// <summary>
/// Academic Implementation: Modern C# Event-Based Observer
/// 
/// Theoretical Analysis:
/// - Uses C# events for type-safe observer pattern
/// - Automatic memory management through weak references
/// - Thread-safe event handling
/// - Simplified syntax while maintaining pattern benefits
/// </summary>

// Event arguments for stock price changes
public class StockPriceChangedEventArgs : EventArgs
{
    public string Symbol { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }
    public decimal Change => NewPrice - OldPrice;
    public decimal ChangePercent => OldPrice != 0 ? (Change / OldPrice) * 100 : 0;
    
    public StockPriceChangedEventArgs(string symbol, decimal oldPrice, decimal newPrice)
    {
        Symbol = symbol;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

// Modern subject using events
public class ModernStock
{
    private decimal _price;
    
    public string Symbol { get; }
    public decimal Price 
    { 
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldPrice = _price;
                _price = value;
                OnPriceChanged(new StockPriceChangedEventArgs(Symbol, oldPrice, value));
            }
        }
    }
    
    // Event declaration - compiler generates add/remove methods
    public event EventHandler<StockPriceChangedEventArgs> PriceChanged;
    
    public ModernStock(string symbol, decimal initialPrice)
    {
        Symbol = symbol;
        _price = initialPrice;
    }
    
    protected virtual void OnPriceChanged(StockPriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}

// Modern observer using event subscription
public class ModernPortfolioTracker
{
    private readonly string _portfolioName;
    private readonly Dictionary<string, int> _holdings;
    
    public ModernPortfolioTracker(string portfolioName)
    {
        _portfolioName = portfolioName;
        _holdings = new Dictionary<string, int>();
    }
    
    public void AddHolding(string symbol, int shares)
    {
        _holdings[symbol] = _holdings.GetValueOrDefault(symbol, 0) + shares;
    }
    
    public void Subscribe(ModernStock stock)
    {
        stock.PriceChanged += OnStockPriceChanged;
    }
    
    public void Unsubscribe(ModernStock stock)
    {
        stock.PriceChanged -= OnStockPriceChanged;
    }
    
    private void OnStockPriceChanged(object sender, StockPriceChangedEventArgs e)
    {
        if (_holdings.TryGetValue(e.Symbol, out int shares))
        {
            var oldValue = e.OldPrice * shares;
            var newValue = e.NewPrice * shares;
            var change = newValue - oldValue;
            
            Console.WriteLine($"Portfolio '{_portfolioName}': {e.Symbol} " +
                            $"changed {e.ChangePercent:F1}%. Portfolio impact: ${change:F2}");
        }
    }
}
```

### Academic Usage Analysis
```csharp
/// <summary>
/// Academic Usage Example: Demonstrating Observer Pattern Benefits
/// 
/// This example shows how the observer pattern enables:
/// - Loose coupling between subjects and observers
/// - Dynamic observer registration/deregistration
/// - Broadcast notifications to multiple observers
/// - Extensibility through new observer types
/// </summary>
public class StockMarketSimulation
{
    public void RunSimulation()
    {
        // Create stocks (subjects)
        var appleStock = new Stock("AAPL", 150.00m);
        var microsoftStock = new Stock("MSFT", 300.00m);
        
        // Create observers
        var portfolio1 = new PortfolioTracker("Tech Portfolio");
        var portfolio2 = new PortfolioTracker("Diversified Portfolio");
        var alertSystem = new PriceAlertSystem();
        var tradingBot = new TradingBot("Momentum Bot", 5.0m, 3.0m);
        
        // Configure portfolios
        portfolio1.AddHolding("AAPL", 100);
        portfolio1.AddHolding("MSFT", 50);
        portfolio2.AddHolding("AAPL", 200);
        
        // Configure alerts
        alertSystem.SetAlert("AAPL", 140.00m, 160.00m);
        alertSystem.SetAlert("MSFT", 280.00m, 320.00m);
        
        // Register observers with subjects
        appleStock.Attach(portfolio1);
        appleStock.Attach(portfolio2);
        appleStock.Attach(alertSystem);
        appleStock.Attach(tradingBot);
        
        microsoftStock.Attach(portfolio1);
        microsoftStock.Attach(alertSystem);
        microsoftStock.Attach(tradingBot);
        
        // Simulate price changes - all observers are notified automatically
        Console.WriteLine("=== Market Opening ===");
        appleStock.Price = 155.50m;  // Triggers notifications to all AAPL observers
        microsoftStock.Price = 305.25m;  // Triggers notifications to all MSFT observers
        
        Console.WriteLine("\n=== Market Volatility ===");
        appleStock.Price = 142.75m;  // Should trigger low price alert
        microsoftStock.Price = 325.00m;  // Should trigger high price alert
        
        // Dynamic observer management
        Console.WriteLine("\n=== Portfolio Rebalancing ===");
        appleStock.Detach(portfolio2);  // Portfolio 2 no longer tracks AAPL
        appleStock.Price = 148.00m;  // Only portfolio1, alerts, and bot are notified
        
        // Demonstrate modern event-based approach
        Console.WriteLine("\n=== Modern Event-Based Approach ===");
        var modernStock = new ModernStock("GOOGL", 2500.00m);
        var modernPortfolio = new ModernPortfolioTracker("Modern Portfolio");
        modernPortfolio.AddHolding("GOOGL", 10);
        
        modernPortfolio.Subscribe(modernStock);
        modernStock.Price = 2550.00m;  // Event-based notification
        modernPortfolio.Unsubscribe(modernStock);
        modernStock.Price = 2600.00m;  // No notification after unsubscribe
    }
}
```

### Academic Analysis of Implementation Benefits
**Theoretical Advantages Demonstrated**:
1. **Loose Coupling**: Subjects don't know concrete observer types
2. **Dynamic Relationships**: Observers can be added/removed at runtime
3. **Broadcast Communication**: One change notifies multiple observers
4. **Extensibility**: New observer types can be added without changing subjects
5. **Separation of Concerns**: Each observer handles its specific responsibility

## Real-world / Enterprise Use Case
- Event bus in microservices
- UI event handling
- ASP.NET Core IHostedService notifications

## Pros and Cons
**Pros:**
- Decouples subject and observers
- Supports event-driven design
**Cons:**
- Can be hard to debug
- Risk of memory leaks if not managed

## Common Mistakes & Anti-Patterns
- Not detaching observers
- Creating circular dependencies

## Performance Considerations
- Minimal overhead for small lists
- Can be slow with many observers

## Relation to SOLID Principles
- Supports OCP (add new observers)
- Can violate SRP if subject manages too much

## Interview Cross-Questions with Answers
- **Q:** How does Observer differ from Mediator?
  **A:** Observer notifies all dependents; Mediator centralizes communication.
- **Q:** Where is Observer used in .NET?
  **A:** Events, delegates, IObservable<T>.

## Quick Revision / Summary
- One-to-many notifications
- Decouples subject and observers
- Used in events and messaging
