=======================
Test-Driven Development

* TDD is easy, but you have to be ready to work at it. You must know your stuff. Think about
  what we're trying to do: we have to know our inputs, what the class(es) are going to look
  like, and what our target outputs are going to be. You must set up your problem so that the
  only thing you're concerned about is your business problem.

* Suppose you're writing a program that will interact with a CC processor. These are "open"
  web API. They're services, but they aren't "traditional" .NET services. You can't "right-click
  -> Add Web Service Reference...". They aren't ASMX or WCF, they're just open API endpoints.

  You need to know how to create an HTTP request, write to the request stream (base64
  encoded JSON), add authorization headers, send an request asynchronously, base64 decode the
  response stream to a string and JSON deserialize the object.

  System.Net.HttpWebRequest
  System.Net.HttpWebResponse
  System.Net.HttpHeaderCollection
  JavaScriptSerializer / JsonDataContactSerializer / Newtonsoft's JSON.Net
  ASCII -> Base64 encoding
  Base64 -> ASCII decoding

  None of that is particularly hard, but if you've never done it before, then you can't TDD
  a solution.

  So, I like junk projects! I create lots of them. I don't practice in my production source.


==============
Types of Tests

* Unit
* Surface
* Integration
* Functional


=============================
The .NET Framework is BORING!

* It doesn't do anything special. It is a way to interact with machines, servers, and web sites.

* All of these things are environment-specific problems.

* We need ways to isolate business rules from the environment.


===============
Adapter Pattern

* Do not be intmidated by design patterns! They are are your friends. Learn to recognize them.

* In this project alone, we will see

  - Adapter
  - Factory & Abstract Factory
  - Singleton
  - Strategy

* You've probably used many design patterns without ever knowing about it. If you've ever used an
  interface, that's an example of the "Strategy" pattern. All the Strategy pattern tries to do is
  separate the what needs to be done with an interchangable "how" something gets done.

* Call things by their proper names! Design patterns will teach you how to read code. Uncle Bob's
  first chapter in clean code is all about good naming. If we see something called a "Builder", 
  "Factory", "Adapter", "Strategy", "Policy", "Decorator", etc. we should have clues to what that
  class does without even opening up the file.

* The Adapter is also called the "Wrapper", and that's exactly what it does. It replaces one class
  with another, usually so we can swap out an implementation at run time.

* The Adapter pattern also makes testing possible where it once was not. Or maybe it was possible,
  but it was incredibly fragile.

* If you wrap multiple classes with a single class, we call that a facade.



