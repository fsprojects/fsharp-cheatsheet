F# Cheatsheet
=============

Comments
--------
Block comments are placed between `(*` and `*)`. Line comments start from `//` and continue until the end of the line.

	(* This is block comment *)

    // And this is line comment
    
Xml doc comments follow `///`, that allow developers to use Xml tags to generate documentation.    
    
    /// Double-backticks are placed between a pair of ``
    let ``1 + 1 should be equal to 2``() =
  		1 + 1 = 2

Strings
-------
In F# `string` is the shortcut for `System.String` type.

    /// Create a string using concatenation operator
    let hello = "Hello" + " World"

*Verbatim strings* preceding by `@` symbol avoid escaping control characters (except using `""` to represent `"`).

    let xml = @"<book title=""Paradise Lost"">"

F# 3.0 has *triple-quoted strings* allowing us not to escape `"`.

    let xml = """<book title="Paradise Lost">"""

*Backslash strings* indent string contents by stripping leading spaces.

    let poem = 
        "The lesser world was daubed\n\
         By a colorist of modest skill\n\
         A master limned you in the finest inks\n\
         And with a fresh-cut quill.\n"

Basic Types and Literals
------------------------

Tuples and Records
------------------

Discriminated Unions
--------------------

Arrays, Lists and Sequences
---------------------------

Pattern Matching
----------------

Function Composition and Pipelining
-----------------------------------

Classes and Inheritance
-----------------------

Interface and Object Expressions
----------------------------

Namespaces and Modules
----------------------

Async Workflows
---------------

Active Patterns
---------------

Compiler Directives
-------------------


