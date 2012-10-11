Types of Tests

* Unit
* Surface
* Integration
* Functional

The .NET Framework is BORING!

* It doesn't do anything special. It is a way to interact with machines, servers, and web sites.

* All of these things are environment-specific problems.

* We need ways to isolate business rules from the environment.

Adapter Pattern

* Do not be intmidated by design patterns! They are are your friends. Learn to recognize them.

* Call things by their proper names! Design patterns will teach you how to read code. Uncle Bob's
  first chapter in clean code is all about good naming. If we see something called a "Builder", 
  "Factory", "Adapter", "Strategy", "Policy", "Decorator", etc. we should have clues to what that
  class does without even opening up the file.

* The Adapter is also called the "Wrapper", and that's exactly what it does. It replaces one class
  with another, usually so we can swap out an implementation at run time.

* The Adapter pattern also makes testing possible where it once was not.


