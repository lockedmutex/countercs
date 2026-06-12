project('countercs', 'cs',
  version: '0.1.0',
  meson_version: '>= 0.59.0'
)

dotnet = find_program('dotnet', required: true)

# Define the path to the main executable project
app_project = meson.project_source_root() / 'src' / 'suyogtandel.countercs' / 'suyogtandel.countercs.csproj'

# A run target to launch the application (equivalent to `make run`)
# Usage: meson compile run -C builddir
run_target('run',
  command: [dotnet, 'run', '--project', app_project]
)

# A custom target to build the .NET solution into an output directory
# Usage: meson compile build-dotnet -C builddir
custom_target('build-dotnet',
  build_by_default: true,
  output: 'dotnet-build-output',
  command: [dotnet, 'build', app_project, '-c', 'Debug']
)
