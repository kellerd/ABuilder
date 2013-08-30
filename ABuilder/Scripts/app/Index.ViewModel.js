$.getScript("Index.SingleModelStatsViewModel.js");

function ViewModel() {
    var self = this;

    var mapping = {
        'Stats': {
            create: function (options) {return new SingleModelStatsViewModel(options.data);}
        }
    }

    
    self.RefreshModels = function () {
        $.getJSON("api/SingleModel", function (data) { ko.mapping.fromJS(data, mapping, self.SingleModels); }
                  );
        return self;
    }

    self.RefreshStats = function () {
        $.getJSON("api/Stat", function (data) { ko.mapping.fromJS(data, {}, self.Stats); }
                  ); return self;
    }

    self.RefreshEquation = function () {
        $.getJSON("api/Equation", function (data) { ko.mapping.fromJS(data, { create: function (options) { return new EquationViewModel(options.data); }}, self.Equations); }
                  );
        return self;
    }

    self.Refresh = function () {
        self.RefreshModels(); self.RefreshStats(); self.RefreshEquation(); return self;
    }

    self.NewName = ko.observable();
    self.NewStat = ko.observable();
    self.SingleModels = ko.mapping.fromJS([]);
    self.Stats = ko.mapping.fromJS([]);
    self.Equations = ko.mapping.fromJS([]);

    self.AddingFunction = ko.observable(false);
    
    self.EquationText = ko.observable("");
    self.EquationId = ko.observable("");
    this.saveEquation = function () {
        if (self.AddingFunction) self.addEquation();
        else self.updateEquation()
    }

    self.removeSingleModel = function (SingleModel) {
        
        for (var i = 0; i < SingleModel.Stats.length;i++)
            self.removeSingleModelStat(SingleModel, Stat);
        self.SingleModels.remove(SingleModel);
        $.ajax({
            url: "api/SingleModel/" + SingleModel.Id(),
            type: "delete",
            success: function (response) {
                //self.RefreshModels();
            }, error: function (response) {
                alert(response.Status);
            }
        });
    };

    self.removeSingleModelStat = function (SingleModel, Stat) {
        $.ajax({
            url: "api/SingleModel_Stat/" + SingleModel.Id() + "/" + Stat.Id(),
            type: "delete",
            success: function (response) {
                //self.RefreshEquation();
            }, error: function (response) {
                alert(response.Status);
            }
        });
    };

    self.removeEquation = function (Equation) {
        self.Equations.remove(Equation);
        $.ajax({
            url: "api/Equation/" + Equation.Id(),
            type: "delete",
            success: function (response) {
                //self.RefreshEquation();
            }, error: function (response) {
                alert(response.Status);
            }
        });
    };

    self.addEquation = function () {
        $.ajax({
            url: "api/Equation",
            type: "post",
            contentType: "application/json",
            data: ko.toJSON({ Value: self.EquationText }),
            success: function (response) {
                self.RefreshEquation();
                self.NewName("");
            }, error: function (response) {
                alert(response.Status);
            }

        });
    };



    self.addModel = function () {
        $.ajax({
            url: "api/SingleModel",
            type: "post",
            contentType: "application/json",
            data: ko.toJSON({ Name: self.NewName }),
            success: function (response) {
                self.Refresh();
                self.NewName("");
            }, error: function (response) {
                alert(response.Status); 
            }
            
        });
    };
  
    self.addStat = function () {
        $.ajax({
            url: "api/Stat",
            type: "post",
            contentType: "application/json",
            data: ko.toJSON({ Acronym: self.NewStat }),
            success: function (response) {
                self.Refresh();
                self.NewStat("");
            }, error: function (response) {
                alert(response.Status);
            }

        });
    };


    var SingleModelStatsViewModel = function (data) {
        ko.mapping.fromJS(data, {}, this);
        this.editable = ko.observable(false);
        this.saveItem = function () {
            self.updateSingleModelStat(this);
        }
    };

    var EquationViewModel = function (data) {
        var eq = this;
        ko.mapping.fromJS(data, {}, this);
        this.editable = ko.observable(false);
        this.saveItem = function () {
            self.updateEquation(this);
        }
        this.evaluate = ko.computed(function() {
            try {
                var result;
                $.ajax({
                    type: 'GET',
                    url: "home/Equation",
                    dataType: 'json',
                    success: function (d) {
                        result = d;
                    },
                    data: { EquationId: eq.Id() },
                    async: false
                });
                return result;
            } catch (e) {
                return "Function not valid";
            }
        }, this);
    }
    self.addSingleModelStat = function (SingleModel, Stat) {
        $.ajax({
            url: "api/SingleModel_Stat",
            type: "post",
            contentType: "application/json",
            data: ko.toJSON({ StatId: Stat.Id, SingleModelId: SingleModel.Id, Value: 0 }),
            success: function (response) {
                self.RefreshModels();
            }, error: function (response) {
                alert(response.Status);
            }

        });
    };
    self.updateSingleModelStat = function (SingleModelStatsViewModel) {
        $.ajax({
            url: "api/SingleModel_Stat/" + SingleModelStatsViewModel.SingleModelId() + "/" + SingleModelStatsViewModel.StatId(),
            type: "put",
            contentType: "application/json",
            data: ko.toJSON(SingleModelStatsViewModel),
            success: function (response) {
                //self.RefreshModels();
            }, error: function (response) {
                alert(response.Status);
            }

        });
    };

    self.updateEquation = function (Equation) {
        $.ajax({
            url: "api/Equation/" + Equation.Id(),
            type: "put",
            contentType: "application/json",
            data: ko.toJSON(Equation),
            success: function (response) {
                self.RefreshEquation();
            }, error: function (response) {
                alert(response.Status);
            }

        });
    };

    self.displaySingleModelStat = function (SingleModel, Stat) {
        if (this.getSingleModelStat(SingleModel, Stat) !== null)
            return true;
        else
            return false;
    }
    self.getSingleModelStat = function (SingleModel, Stat) {
        for (var i = 0; i < SingleModel.Stats().length; i++)
            if (SingleModel.Stats()[i].StatId() == Stat.Id())
                return SingleModel.Stats()[i];
        return null;
    }
}