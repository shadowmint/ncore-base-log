# NCore.Base.Log

Setup basic logging easily, instead of copy-pasting the NLog config every time.

    new LogService().ConfigureLogging();

or:

    public class MyConfigProvider : DefaultConfigBuilder
    {
        public override bool EnableConsoleLogging => false;
    }

    new LogService().ConfigureLogging<MyConfigProvider>();

# Installing

    npm install --save shadowmint/ncore-base-log

Now add the `NuGet.Config` to the project folder:

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
     <packageSources>
        <add key="local" value="./packages" />
     </packageSources>
    </configuration>

Now you can install the package:

    dotnet add package NCore.Base.Log

You may also want to use `dotnet nuget locals all --clear` to clear cached objects.

# Building a new package version

    npm run build

# Testing

    npm test
