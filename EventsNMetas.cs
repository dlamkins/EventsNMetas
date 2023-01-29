using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Blish_HUD.Modules;
using Blish_HUD.Settings;
using EventsNMetas.Settings;
using Microsoft.Xna.Framework;

namespace EventsNMetas {

    [Export(typeof(Module))]
    public class EventsNMetas : Module {

        private readonly DependencyContainer _moduleDep;

        private ModuleSettings _settings;

        [ImportingConstructor]
        public EventsNMetas([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) {
            _moduleDep = new DependencyContainer().With(moduleParameters.DirectoriesManager)
                                                  .With(moduleParameters.SettingsManager)
                                                  .With(moduleParameters.DirectoriesManager)
                                                  .With(moduleParameters.ContentsManager)
                                                  .With(moduleParameters.Gw2ApiManager)
                                                  .With(moduleParameters.Manifest);
        }

        protected override void DefineSettings(SettingCollection settings) {
            _settings = new ModuleSettings(settings);
            _moduleDep.With(settings);
        }

        protected override async Task LoadAsync() {

        }

        protected override void Update(GameTime gameTime) {
            // Do something
        }

        protected override void Unload() {
            _moduleDep.Terminate();
        }

    }
}
