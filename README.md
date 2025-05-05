<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>


# Emovere Microsservices - Emovere Core

<p>
  <strong>Emovere-Core</strong> is a shared library used across all microservices in the <strong>Emovere</strong> ecosystem. It provides a common foundation for domain modeling, communication, and infrastructure, helping enforce consistency and reusability throughout the distributed architecture.
</p>

<h2>🧱 Projects Structure</h2>

<h3>📌 Emovere.SharedKernel</h3>
<p>
  Contains domain-related base classes and core design patterns, including:
</p>
<ul>
  <li><strong>Entity base class</strong> - For domain entities with identity and equality logic</li>
  <li><strong>AssertionConcern</strong> - For enforcing domain validations with expressive error handling</li>
  <li><strong>Domain Events</strong> - Abstract base classes to implement domain-driven event handling</li>
  <li><strong>Use Case Abstractions</strong> - Base interfaces and patterns to structure input/output flows</li>
  <li><strong>Notification Pattern</strong> - A centralized pattern for handling domain validation errors</li>
</ul>

<h3>🧩 Emovere.Infrastructure</h3>
<p>
  Contains reusable infrastructure implementations such as:
</p>
<ul>
  <li><strong>Resilient messaging abstraction</strong> for queue-based communication, with support for <strong>Circuit Breaker</strong> and <strong>Retry</strong> mechanisms — enabling Event-Driven Architecture (EDA) and support for Sagas</li>
  <li><strong>Event Sourcing support</strong> with a shared implementation of <strong>EventStoreDB</strong></li>
  <li><strong>SendGrid integration</strong> for email delivery</li>
</ul>

<h3>🔄 Emovere.Communication</h3>
<p>
  Provides a centralized set of <strong>Integration Events</strong> used across microservices to standardize inter-service communication and message contracts.
</p>
