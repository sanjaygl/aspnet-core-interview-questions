# Generative AI & GitHub Copilot – Advanced Interview Questions (100+)

## 1. What is Generative AI (GenAI)?

Generative AI refers to AI systems that can **create new content** such as text, code, images, or audio using learned patterns from large datasets.

**Example:**
* Writing C# code from a natural language prompt
* Generating unit tests automatically

---

## 2. Is GitHub Copilot a Generative AI tool?

Yes. GitHub Copilot uses **Large Language Models (LLMs)** to generate new code, comments, tests, and explanations.

---

## 3. What type of AI model powers Copilot?

Copilot is powered by **LLMs (Large Language Models)** trained and fine-tuned for **software development tasks**.

---

## 4. How is Copilot different from IntelliSense?

| IntelliSense | Copilot |
|-------------|---------|
| Rule-based | GenAI-based |
| Suggests known APIs | Generates new logic |
| Syntax-aware | Context + intent aware |

---

## 5. What is an LLM?

An LLM is a deep learning model trained on massive text/code data to understand and generate human-like responses.

---

## 6. What data is Copilot trained on?

* Publicly available code
* Open-source repositories
* Documentation and programming patterns

---

## 7. Does Copilot understand project context?

Yes. Copilot reads:
* Open files
* File structure
* Imports
* Naming conventions

---

## 8. What are Copilot modes?

* **Ask** – Explain & answer
* **Edit** – Modify selected code
* **Agent** – Multi-file autonomous tasks

---

## 9. What is Copilot Ask mode?

Ask mode provides **read-only explanations** without modifying code.

**Example:**

> Explain this LINQ query

---

## 10. What is Copilot Edit mode?

Edit mode applies **targeted changes** to selected code.

**Example:**

> Refactor this method to async

---

## 11. What is Copilot Agent mode?

Agent mode can:
* Analyze the project
* Plan changes
* Modify multiple files

**Example:**

> Apply Clean Architecture to this API

---

## 12. Can Copilot write unit tests?

Yes.

**Example:**

```csharp
// Prompt: Generate NUnit tests for this service
```

---

## 13. What is MCP (Model Context Protocol) in GitHub Copilot?

MCP is a protocol that enables Copilot to **connect to external data sources and tools** beyond the local workspace. It allows:
* Integration with external APIs
* Database connections for schema awareness
* Custom tool integrations
* Real-time data fetching

**Use Case:**
Connect Copilot to your production database schema so it can generate accurate Entity Framework models based on actual table structures.

---

## 14. How do you configure MCP servers in Copilot?

MCP servers are configured in the Copilot settings JSON:

```json
{
  "github.copilot.mcp": {
    "servers": {
      "database-schema": {
        "command": "node",
        "args": ["path/to/mcp-server.js"],
        "env": {
          "DB_CONNECTION": "Server=localhost;Database=MyDb"
        }
      }
    }
  }
}
```

---

## 15. What is the difference between GPT-3.5-turbo and GPT-4 in Copilot context?

| Aspect | GPT-3.5-turbo | GPT-4 |
|--------|---------------|-------|
| Response Speed | Faster (1-2s) | Slower (3-5s) |
| Context Window | 4K-16K tokens | 8K-32K tokens |
| Code Accuracy | Good for simple tasks | Better for complex logic |
| Cost | Lower | Higher |
| Reasoning | Basic | Advanced reasoning |

**When to use GPT-4:**
* Complex refactoring
* Architecture decisions
* Multi-step problem solving

---

## 16. How does token counting work in Copilot, and why does it matter?

Tokens are pieces of text (roughly 4 characters). Copilot has a **context window limit**:
* GPT-3.5: ~4, 000-16, 000 tokens
* GPT-4: ~8, 000-32, 000 tokens

**Why it matters:**
If your prompt + context exceeds the limit, Copilot will truncate older context, potentially losing important information.

**Example calculation:**

```
Input file: 500 lines × 50 chars/line = 25,000 chars ≈ 6,250 tokens
Your prompt: 100 chars ≈ 25 tokens
Total: ~6,275 tokens (fits in GPT-4, might truncate in GPT-3.5)
```

---

## 17. Explain the role of temperature and top_p in Copilot's model settings

**Temperature (0.0 - 1.0):**
* Controls randomness/creativity
* 0.0 = Deterministic, same output every time
* 0.7 = Balanced
* 1.0 = Very creative, unpredictable

**Top_p (0.0 - 1.0) - Nucleus Sampling:**
* Controls diversity by probability mass
* 0.1 = Only top 10% probable tokens considered
* 0.9 = Top 90% considered

**Best practices:**

```json
{
  "github.copilot.advanced": {
    "temperature": 0.2,  // For code generation (need consistency)
    "top_p": 0.95
  }
}
```

For unit tests or documentation, you might use higher temperature (0.5-0.7) for variety.

---

## 18. What is the max_tokens setting in Copilot, and how do you optimize it?

**max_tokens** limits the length of Copilot's response.

**Default:** Usually 1000-2000 tokens

**Optimization strategies:**

```json
{
  "github.copilot.advanced": {
    "max_tokens": 500  // For quick suggestions
  }
}
```

**When to increase:**
* Generating entire classes
* Complex refactoring
* Multiple method generation

**When to decrease:**
* Inline completions
* Simple fixes
* Faster response times

---

## 19. How do you handle Copilot suggestions that use deprecated . NET APIs?

**Scenario:** Copilot suggests `WebClient` instead of `HttpClient` in . NET code.

**Solutions:**
1. **Explicit context in comments:**

```csharp
// Use HttpClient (not WebClient) to fetch data from API
// Follow .NET 6+ best practices
```

2. **Provide modern example:**

```csharp
// Example: Use HttpClient with dependency injection
private readonly HttpClient _httpClient;

public UserService(HttpClient httpClient)
{
    _httpClient = httpClient;
}
```

3. **Use @workspace with modern files:**
Open recent code files using current patterns so Copilot learns from them.

4. **Configure settings to prefer newer models:**

```json
{
  "github.copilot.advanced": {
    "model": "gpt-4-turbo"
  }
}
```

---

## 20. Describe a scenario where Copilot's context window limitation caused issues and how you resolved it

**Scenario:**
Working on a large service class (800 lines) and asking Copilot to refactor a method. Copilot generated code that broke dependencies because it couldn't see private fields defined earlier.

**Resolution:**
1. **Split the request:**
   - First: Extract just the method and its dependencies to a new file
   - Then: Ask for refactoring
   - Finally: Merge back

2. **Use @workspace + specific instructions:**

```
@workspace Consider the ILogger and IConfiguration dependencies 
when refactoring this method
```

3. **Provide explicit context:**

```csharp
// Available dependencies:
// - private readonly ILogger<UserService> _logger;
// - private readonly IConfiguration _config;
// - private readonly IUserRepository _repo;

// Refactor this method:
public async Task<User> GetUserAsync(int id) { ... }
```

---

## 21. What are embeddings in the context of Copilot, and how do they improve suggestions?

**Embeddings** are vector representations of code that capture semantic meaning.

**How Copilot uses them:**
1. Converts your code and comments into vectors
2. Finds similar patterns in training data
3. Ranks suggestions by vector similarity

**Example:**

```csharp
// Copilot embeds this comment:
// "Calculate user's age from birthdate"

// Finds similar vectors from training data:
// - "compute age given date of birth"
// - "determine years old from birthday"

// Returns relevant code even if exact words don't match
```

**Advanced usage:**
Use semantically rich comments to get better suggestions:

```csharp
// BETTER: "Implement exponential backoff retry logic with 3 attempts"
// WORSE: "Add retry"
```

---

## 22. How does Copilot handle multi-repository context in a monorepo setup?

**Challenge:** Copilot's context is limited to open files and workspace indexing.

**Strategies for monorepo:**

1. **Use workspace symbols:**

```
@workspace How is authentication implemented across all microservices?
```

2. **Open relevant files:**
Before asking, open key files from different services:
* `auth-service/AuthController.cs`
* `user-service/UserController.cs`
* `shared/SecurityMiddleware.cs`

3. **Explicit cross-service references:**

```csharp
// This service follows the same auth pattern as 
// ../auth-service/Controllers/AuthController.cs
// Generate similar JWT validation middleware
```

4. **Use MCP to index entire monorepo:**
Configure an MCP server that maintains a searchable index of all services.

---

## 23. Explain how Copilot's fine-tuning works in enterprise scenarios

**Standard Copilot:** Trained on public code only.

**Copilot Enterprise with custom models:**
1. **Data collection:** Your organization's private repositories
2. **Fine-tuning process:**
   - Extract code patterns
   - Train on your coding standards
   - Learn internal libraries/frameworks
3. **Deployment:** Custom model endpoint for your organization

**Benefits:**
* Suggests company-specific patterns
* Uses internal framework names
* Follows organizational coding standards
* Understands proprietary APIs

**Example:**

```csharp
// If your company has an internal framework "AcmeAuth"
// Standard Copilot: Suggests generic auth
// Fine-tuned Copilot: Suggests AcmeAuth.Authenticate()
```

---

## 24. What is the difference between Copilot's completion engine and chat engine?

| Aspect | Completion Engine | Chat Engine |
|--------|-------------------|-------------|
| Trigger | Typing code | Explicit chat prompt |
| Context | Current file primarily | Can use @workspace |
| Response | Code snippets | Code + explanation |
| Model | Optimized for speed | Optimized for reasoning |
| Token budget | Lower (faster) | Higher (detailed) |

**Internal architecture:**
* **Completion:** Uses lighter model (Codex/GPT-3.5) with aggressive caching
* **Chat:** Can use GPT-4 with broader context

---

## 25. How do you debug why Copilot is giving poor suggestions for your code?

**Diagnostic steps:**

1. **Check context visibility:**

```
Open Copilot output panel (View → Output → GitHub Copilot)
Look for: "Context: [list of files considered]"
```

2. **Verify token usage:**

```
If context shows truncation warnings, reduce open files or use more specific prompts
```

3. **Test prompt variations:**

```csharp
// Poor: "make this better"
// Better: "refactor to async/await pattern with cancellation token"
// Best: "convert to async/await using IAsyncEnumerable for streaming results"
```

4. **Check model selection:**

```json
{
  "github.copilot.advanced": {
    "debug": true,  // Enables detailed logging
    "model": "gpt-4"  // Try different model
  }
}
```

5. **Analyze training data bias:**
If Copilot consistently suggests outdated patterns, it may have been trained on older code. Use explicit version constraints:

```csharp
// Use .NET 8 minimal API pattern (not controller-based)
```

---

## 26. Describe how Copilot's ranking algorithm prioritizes suggestions

**Ranking factors (in order):**

1. **Syntax validity** (40%): Does it compile?
2. **Context match** (30%): Matches file imports, variable names
3. **Pattern frequency** (15%): How common is this pattern in training data
4. **Type safety** (10%): Correct types and signatures
5. **Recency bias** (5%): Prefers recently typed patterns

**Example:**

```csharp
public class UserService
{
    private readonly IUserRepository _repo;
    
    // Typing: public async Task<User> GetUser
    
    // Copilot ranks:
    // Rank 1: (int id) → uses existing _repo, matches async pattern
    // Rank 2: (string email) → matches async, different param
    // Rank 3: () → syntactically correct but less contextual
}
```

---

## 27. What is Copilot's approach to handling PII (Personally Identifiable Information)?

**Detection mechanisms:**
1. Pattern matching for:
   - Email addresses
   - Phone numbers
   - SSN/Credit cards
   - API keys (regex patterns)

2. **Content filtering:**

```csharp
// Copilot will AVOID suggesting:
var apiKey = "sk-1234567890abcdef";  // Looks like real key
var ssn = "123-45-6789";              // Valid SSN format

// Copilot will SUGGEST:
var apiKey = configuration["ApiKey"];  // From config
var ssn = user.SocialSecurityNumber;   // From variable
```

3. **Telemetry filtering:**
Even with telemetry enabled, Microsoft filters:
* Secrets
* PII
* Proprietary business logic patterns

**Best practice:**
Never commit real credentials. Use:

```json
{
  "github.copilot.advanced": {
    "filterSensitiveData": true
  }
}
```

---

## 28. How does Copilot handle code completion in polyglot projects (e.g., C# + TypeScript)?

**Context switching:}

Copilot maintains **separate context windows** per file type:

**Example project:**

```
/backend (C#)
  - UserController.cs
  
/frontend (TypeScript)
  - user.service.ts
```

**When editing `user.service.ts` :**

```typescript
// Copilot sees:
// 1. Current TypeScript file
// 2. Other open .ts files
// 3. package.json dependencies
// 4. Imported modules

// Copilot does NOT directly use C# context unless:
// - You explicitly reference it
// - You use @workspace
```

**Cross-language awareness:**

```typescript
// Fetch user from C# API endpoint /api/users/{id}
// Backend UserController.GetUser returns: { id, name, email, createdDate }

async getUser(id: number) {
  // Copilot will infer the response shape from your comment
  const response = await fetch(`/api/users/${id}`);
  return await response.json() as User;
}
```

---

## 29. What is the role of the `github.copilot.advanced.inlineSuggestCount` setting?

**Purpose:** Controls how many alternative suggestions Copilot generates.

```json
{
  "github.copilot.advanced": {
    "inlineSuggestCount": 3
  }
}
```

**Behavior:**
* Default: 1 (show best suggestion)
* 3: Generate 3 alternatives, rank them, show best
* 10: More options (slower but potentially better)

**Trade-offs:**
| Count | Latency | Quality | Use Case |
|-------|---------|---------|----------|
| 1 | ~1s | Good | Fast coding |
| 3 | ~2s | Better | Normal use |
| 10 | ~4s | Best | Complex algorithms |

**Cycling through suggestions:**
* `Alt + ]` - Next suggestion
* `Alt + [` - Previous suggestion

---

## 30. How do you configure Copilot to respect your team's coding standards?

**Multi-layered approach:**

**1. EditorConfig:**

```ini
# .editorconfig
[*.cs]
indent_style = space
indent_size = 4
dotnet_naming_rule.private_fields.severity = error
dotnet_naming_rule.private_fields.symbols = private_fields
dotnet_naming_rule.private_fields.style = underscore_prefix
```

Copilot reads `.editorconfig` and adapts formatting.

**2. Code examples in workspace:**

```csharp
// Keep exemplar files open that demonstrate standards
// Copilot learns from them

// Good example file: Services/ExampleService.cs
public class ExampleService : IExampleService
{
    private readonly ILogger<ExampleService> _logger;
    
    public ExampleService(ILogger<ExampleService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<Result<User>> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        // Team standard: Use Result<T> pattern
        // Team standard: Always include CancellationToken
        // Team standard: Validate parameters
    }
}
```

**3. Instruction comments:**

```csharp
// TEAM STANDARD: All public methods must have XML comments
// TEAM STANDARD: Use Result<T> for error handling, not exceptions
// TEAM STANDARD: Inject ILogger<T> for logging

/// <summary>
/// Creates a new user account
/// </summary>
public async Task<Result<User>> CreateUserAsync(CreateUserDto dto)
{
    // Copilot now knows to follow these patterns
}
```

**4. Custom Copilot instructions (if using Enterprise):**

```json
{
  "github.copilot.instructions": [
    "Always use var for local variables",
    "Prefer async/await over Task.Result",
    "Use guard clauses for validation",
    "Log errors with structured logging"
  ]
}
```

---

## 31. Explain Copilot's behavior with test-driven development (TDD)

**TDD workflow with Copilot:**

**Step 1: Write the test first**

```csharp
[Test]
public async Task CreateUser_WithValidData_ReturnsSuccess()
{
    // Arrange
    var service = new UserService(_mockRepo.Object);
    var dto = new CreateUserDto { Name = "John", Email = "john@example.com" };
    
    // Act
    var result = await service.CreateUserAsync(dto);
    
    // Assert
    Assert.IsTrue(result.IsSuccess);
    Assert.AreEqual("John", result.Value.Name);
}
```

**Step 2: Ask Copilot to generate implementation**

```csharp
// Copilot Chat: @workspace Implement UserService.CreateUserAsync to make this test pass

public async Task<Result<User>> CreateUserAsync(CreateUserDto dto)
{
    // Copilot generates implementation based on test expectations
    var user = new User
    {
        Name = dto.Name,
        Email = dto.Email,
        CreatedDate = DateTime.UtcNow
    };
    
    await _repo.AddAsync(user);
    return Result<User>.Success(user);
}
```

**Advanced TDD technique:**

```csharp
// Write multiple test cases first
[TestCase("", false)]  // Empty name should fail
[TestCase("A", false)] // Too short should fail
[TestCase("ValidName", true)] // Valid should succeed
public async Task CreateUser_ValidatesNameLength(string name, bool expectedSuccess)
{
    // Test implementation
}

// Then ask: Generate CreateUserAsync that satisfies all these test cases
```

---

## 32. What is Copilot's approach to handling breaking changes in dependencies?

**Scenario:** Your project upgrades from . NET 6 to . NET 8.

**Copilot's limitations:**
* May suggest . NET 6 patterns if most training data is older
* Doesn't automatically know your `TargetFramework`

**Solutions:**
**1. Explicit version context:**

```csharp
// Using .NET 8 features
// Target: .NET 8.0
// Use TimeProvider instead of DateTime for testability

public class OrderService
{
    private readonly TimeProvider _timeProvider;
    
    // Copilot will now use .NET 8 patterns
}
```

**2. Open migration guides:**
Open a file with . NET 8 migration notes:

```markdown
# .NET 8 Migration Notes
- Use TimeProvider instead of ISystemClock
- WebApplicationBuilder replaces Host.CreateDefaultBuilder
- Minimal APIs are preferred over Controllers
```

**3. Show examples:**

```csharp
// .NET 8 example - Use this pattern for all services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(TimeProvider.System);

// Now when you ask Copilot to create a new service,
// it will follow this pattern
```

---

## 33. How does Copilot's caching mechanism work, and how can you leverage it?

**Caching layers:**

**1. Local completion cache (30s-2m):**

```csharp
// If you type the same pattern twice quickly:
public void Method1() { var x = 1; }
public void Method2() { var x = 1; } // ← Served from cache, instant
```

**2. Context hash cache:**

```
When context (open files + recent edits) remains same:
- Hash: abc123def456
- Cached suggestions: [...] 
- TTL: 5 minutes
```

**3. Model response cache:**
Similar prompts across users (anonymized) may share cache.

**Leveraging cache for speed:**

```csharp
// Generate multiple similar methods:
public async Task<User> GetUserAsync(int id) { }
// Cache warms up with pattern

public async Task<Order> GetOrderAsync(int id) { } // Fast (similar pattern)
public async Task<Product> GetProductAsync(int id) { } // Very fast (cache hit)
```

**Cache invalidation:**
* File edit
* Different context window
* Model version update
* Explicit clear: Reload VS Code window

---

## 34. Describe how to use Copilot for legacy code modernization at scale

**Real-world scenario:** Modernize 100+ files from . NET Framework to . NET 8

**Systematic approach:**

**Step 1: Create migration template**

```csharp
// BEFORE (.NET Framework)
using System.Web.Mvc;

public class UserController : Controller
{
    public ActionResult Index()
    {
        var users = db.Users.ToList();
        return View(users);
    }
}

// AFTER (.NET 8)
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    
    public UserController(IUserRepository repo) => _repo = repo;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _repo.GetAllAsync();
        return Ok(users);
    }
}
```

**Step 2: Use Copilot Agent for batch processing**

```
@workspace /agent Migrate all controllers in /Controllers folder 
from .NET Framework MVC to .NET 8 Web API following the pattern 
in MigrationTemplate.cs
```

**Step 3: Iterative verification**
```csharp
// After each migration, ask:
@workspace Analyze the migrated UserController for:
1. Missing async/await conversions
2. Removed dependency injection
3. Breaking API changes
4. Security vulnerabilities introduced
