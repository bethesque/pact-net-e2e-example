# Pact-net End to End Example

[![Build Status](https://travis-ci.org/alastairs/pact-net-e2e-example.svg?branch=master)](https://travis-ci.org/alastairs/pact-dotnet-e2e-example)

Code base to use for demonstrating features or recreating issues in the .NET implementation of pact. Please fork it and modify to demonstrate or recreate your own code.

This code base is _not_ intended as an example of a best practice pact implementation. Its purpose is to create a running example with the simplest code possible. If you want to see an example of how you would use pact in a .NET consumer or provider project, see the [example] dir in the pact repository.

## Usage

**Note:** The example repo was built for .NET Core SDK 3.1 LTS release, but broadly speaking it _should_ be
backwards-compatible with the 2.1 LTS SDK and forwards-compatible with the 5.0 preview SDKs.

### Fork and clone the codebase

    # Fork the repository using the 'Fork' button on the repository home page, then:
    git clone git@github.com:YOUR_USERNAME/pact-dotnet-e2e-example.git
    cd pact-dotnet-e2e-example

### Set the package versions you are using

* Open up the `.csproj` files and set the exact NuGet package versions you are using, e.g.
  ```xml
  <PackageReference Include="pactnet" Version="2.5.4" />
  <!--
      Note that, regardless of your own platform, the x64 Linux package is a *minimum*
      requirement for this repo, as this is what the Travis build runs on.
  -->
  <PackageReference Include="pactnet.linux.x64" Version="2.5.4" />
  ```

* Run `dotnet restore`

### Set up consumer and provider

* Modify the code in `consumer/BarClientFacts.cs` to recreate your consumer expectations
  and actual requests.
* Modify the code in `provider/src/BarApp.cs` to recreate the response that your provider
  will return.
* Run the following PowerShell snippet to run the consumer specs, generate the pact file,
  and verify the pact file:
  ```powershell
  "Consumer", "Provider" | ForEach-Object {
    & dotnet test Sample.$_.sln
  }
  ```
* You will find the pact file in `consumer/spec/pacts/foo-bar.json`
* To run just the consumer specs and generate the pact: `dotnet test Sample.Consumer.sln`
* To run just the provider verification: `dotnet test Sample.Provider.sln`

### To recreate issues with a local pact broker

* Follow the instructions on [running the Pact Broker
  locally](https://github.com/pact-foundation/pact_broker#usage)
* Set the `PACT_BROKER_BASE_URI` environment variable to the URL of your local broker instance, e.g.
  `https://localhost:9292`.

### To recreate issues with a remote pact broker

* Set the `PACT_BROKER_BASE_URI` environment variable to the value of your Pact broker, e.g.
  `https://test.pact.dius.com.au`.

* Follow the above instructions for setting up the consumer and provider code.

* Set the `PACT_BROKER_USERNAME` and `PACT_BROKER_PASSWORD` environment variables if you are using a broker with basic auth.

      export PACT_BROKER_USERNAME=your_username
      export PACT_BROKER_PASSWORD=your_password

* In the root directory of this project, run `dotnet test Sample.Provider.sln`

## Reporting an issue

* Commit your code on a branch (so you can reuse it for other issues cleanly) and push it to your fork

      git checkout -b "foo-issue"
      git add .
      git commit -m "Modifying code to recreate my issue"
      git push --set-upstream origin foo-issue

* Open an issue in the appropriate codebase (see [pact-foundation] for most of the repositories) and include
  a link to your branch.

[pact-foundation]: https://github.com/pact-foundation
