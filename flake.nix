{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };

  outputs = { self, nixpkgs }:
    let
      supportedSystems = [
        "x86_64-linux"
        "aarch64-linux"
      ];

      forAllSystems = nixpkgs.lib.genAttrs supportedSystems;
      nixpkgsFor = forAllSystems (system: import nixpkgs { inherit system; });
    in {
      devShells = forAllSystems (system:
        let
          pkgs = nixpkgsFor.${system};

          adwDependencies = with pkgs; [
            gtk4
            libadwaita
            graphene
            harfbuzz
            pango
            cairo
            glib
            gdk-pixbuf
            libGL
            vulkan-loader
          ];
        in {
          default = pkgs.mkShell {
            packages = [
              pkgs.dotnetCorePackages.sdk_10_0-bin
              pkgs.meson
              pkgs.ninja
            ];

            DOTNET_ROOT = "${pkgs.dotnetCorePackages.sdk_10_0-bin}/share/dotnet";

            shellHook = ''
              export LD_LIBRARY_PATH="${pkgs.lib.makeLibraryPath adwDependencies}:$LD_LIBRARY_PATH"
            '';
          };
        }
      );
    };
}
