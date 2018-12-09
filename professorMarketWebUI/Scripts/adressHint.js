(function () {
    function n(a) {
        a = {
            Options: a, jZipField: null, Fields: [], FieldsByLevels: {}, BuildQueryUpTo: function (c) {
                for (var b = "", a = 0, g = 0; g < this.Fields.length; g++)if (f.IsLessOrEqualLevel(this.Fields[g].MinLevel, c.MinLevel)) { var h = this.Fields[g].GetValue(); if (0 < h.length || this.Fields[g] === c) b += f.GetLevelsSigns(this.Fields[g].Levels), b += "[", b += f.EscapeQuery(h), b += "]", a += h.length } else this.Fields[g].ClearValue(); this.jZipField && (c = this.jZipField.val(), b += e.ZipLevel.sign, b = b + "[" + f.EscapeQuery(c), b += "]", a += c.length);
                return 0 < a ? b : ""
            }, IsTailField: function (a) { return null == f.Find(this.Fields, function (b) { return f.IsLessLevel(a.MaxLevel, b.MinLevel) && "" != b.GetValue() }) }, RefreshForm: function (a, b) { this.ClearAllFields(); for (var d = 0; d < b.length; d++)this.FieldsByLevels[b[d].level].AppendValue(b[d].type + " " + b[d].name) }, ClearAllFields: function () { for (var a = 0; a < this.Fields.length; a++)this.Fields[a].ClearValue() }, Init: function () {
                for (var a = this, b = 0; b < this.Options.fields.length; b++)this.Fields.push(p(this, this.Options.fields[b]));
                for (b = 0; b < this.Fields.length; b++)for (Level in e.AddressLevels) f.IsLessOrEqualLevel(this.Fields[b].MinLevel, Level) && f.IsLessOrEqualLevel(Level, this.Fields[b].MaxLevel) && (this.FieldsByLevels[Level] = this.Fields[b]); void 0 !== this.Options.zip_field && (b = this.Options.zip_field.id, this.jZipField = "string" == typeof b && "#" !== b[0] && "." !== b[0] ? $("#" + b) : $(b), this.jZipField.on("keyup" + e.EventsNamespace, function () { a.ClearAllFields() }), this.jZipField.on("input" + e.EventsNamespace, function () { a.ClearAllFields() }), this.jZipField.on("change" +
                    e.EventsNamespace, function () { a.ClearAllFields() }))
            }, Dispose: function () { for (var a = 0; a < this.Fields.length; a++)this.Fields[a].Dispose() }
        }; a.Init(); return a
    } function p(a, c) {
        var b = {
            Options: f.BuildFieldOptions(c, a.Options, "site/suggest/address"), AddressForm: a, Levels: c.levels, MinLevel: "", MaxLevel: "", EXT: {
                PrepareQuery: function (a) { return { query: this.AddressForm.BuildQueryUpTo(this), mode: "discrete", addresslim: this.Options.limit } }, IsAllowedToSuggestOnFocus: function () {
                    return this.AddressForm.IsTailField(this) &&
                        this.Options.suggest_on_focus
                }, IsAllowedQuery: function (a, b) { var c = a.query && 0 < a.query.length ? !0 : !1; c && 0 == b.length && (c = this.Options.suggest_on_empty); return c }, OnChooseSuggestion: function (a) {
                    var b = f.DecodeMachineAddress(a.machine); this.AddressForm.RefreshForm(this, b); this.Options.on_fetch && (b = $.extend({ query: a.sign }, this.AddressForm.Options.api_options), b.output = b.output ? b.output + "|json" : "json", this.Options.user && (b.user = this.Options.user), $.ajax({
                        type: "GET", url: this.Options.ahunter_url + "site/fetch/address",
                        data: b, cache: !1, context: this, success: function (b) { this.Options.on_fetch.call(this.Options.on_fetch_context, a, b.address) }
                    }))
                }
            }, ClearValue: function () { this.ResetField("", !0); this.ClearSuggestions() }, AppendValue: function (a) { var b = this.GetValue(); b && (b += ", "); this.ResetField(b + a, !0) }, Init: function () {
            this.MinLevel = e.AddressLevels.Flat.name; this.MaxLevel = e.AddressLevels.Region.name; for (var a = 0; a < this.Levels.length; a++)f.IsLessLevel(this.Levels[a], this.MinLevel) && (this.MinLevel = this.Levels[a]), f.IsLessLevel(this.MaxLevel,
                this.Levels[a]) && (this.MaxLevel = this.Levels[a]); void 0 === c.empty_msg && this.MinLevel === e.AddressLevels.Flat.name && (this.Options.empty_msg = "")
            }
        }; b.Init(); var d = k(b.Options.id, b.Options.max_height, b.Options.z_index, b.Options.full_url, b.Options.empty_msg, b.Options.append_to); return f.ExtendSuggester(d, b)
    } function q(a) {
        a = {
            Options: f.BuildFieldOptions(a, {}, "site/suggest/address"), EXT: {
                PrepareQuery: function (a) { return { query: a, addresslim: this.Options.limit } }, IsAllowedToSuggestOnFocus: function () { return this.Options.suggest_on_focus },
                OnChooseSuggestion: function (a) { if (this.Options.on_fetch) { var c = $.extend({ query: a.sign }, this.Options.api_options); c.output = c.output ? c.output + "|json" : "json"; this.Options.user && (c.user = this.Options.user); $.ajax({ type: "GET", url: this.Options.ahunter_url + "site/fetch/address", data: c, cache: !1, context: this, success: function (c) { this.Options.on_fetch.call(this.Options.on_fetch_context, a, c.address) } }) } }
            }, Init: function () { }
        }; a.Init(); var c = k(a.Options.id, a.Options.max_height, a.Options.z_index, a.Options.full_url,
            a.Options.empty_msg, a.Options.append_to); return f.ExtendSuggester(c, a)
    } function r(a) {
        a = {
            Options: a, Fields: [], BuildQueryWithActiveField: function (a) { return { active: a.Tag, last_name: this.GetFieldValue("last_name"), first_name: this.GetFieldValue("first_name"), patronym: this.GetFieldValue("patronym") } }, GetFieldValue: function (a) { var b = f.Find(this.Fields, function (b) { return b.Tag == a }); return null != b ? b.GetValue() : "" }, GetPerson: function () {
                for (var a = {
                    last_name: this.GetFieldValue("last_name"), first_name: this.GetFieldValue("first_name"),
                    patronym: this.GetFieldValue("patronym"), full_name: ""
                }, b = 0; b < this.Fields.length; b++)a.full_name.length && this.Fields[b].GetValue().length && (a.full_name += " "), a.full_name += this.Fields[b].GetValue(); return a
            }, Init: function () { for (var a = 0; a < this.Options.fields.length; a++)this.Fields.push(t(this, this.Options.fields[a])) }, Dispose: function () { for (var a = 0; a < this.Fields.length; a++)this.Fields[a].Dispose() }
        }; a.Init(); return a
    } function t(a, c) {
        var b = {
            Options: f.BuildFieldOptions(c, a.Options, "site/suggest/person"),
            PersonForm: a, Tag: c.tag, EXT: { PrepareQuery: function (a) { return $.extend(this.PersonForm.BuildQueryWithActiveField(this), { personlim: this.Options.limit }) }, IsAllowedToSuggestOnFocus: function () { return this.Options.suggest_on_focus }, IsAllowedQuery: function (a, b) { return 0 < b.length }, OnChooseSuggestion: function (a) { this.Options.on_fetch && this.Options.on_fetch.call(this.Options.on_fetch_context, a, this.PersonForm.GetPerson()) } }, Init: function () { }
        }; b.Init(); var d = k(b.Options.id, b.Options.max_height, b.Options.z_index,
            b.Options.full_url, b.Options.empty_msg, b.Options.append_to); return f.ExtendSuggester(d, b)
    } function u(a) {
        a = { Options: f.BuildFieldOptions(a, {}, "site/suggest/person"), EXT: { PrepareQuery: function (a) { return { query: a, personlim: this.Options.limit } }, IsAllowedToSuggestOnFocus: function () { return this.Options.suggest_on_focus }, OnChooseSuggestion: function (a) { this.Options.on_fetch && this.Options.on_fetch.call(this.Options.on_fetch_context, a, { full_name: a.value }) } }, Init: function () { } }; a.Init(); var c = k(a.Options.id, a.Options.max_height,
            a.Options.z_index, a.Options.full_url, a.Options.empty_msg, a.Options.append_to); return SolidSuggester = f.ExtendSuggester(c, a)
    } function k(a, c, b, d, g, h) {
        a = {
            EXT: { PrepareQuery: function (a) { return { query: a } }, IsAllowedToSuggestOnFocus: function () { return !0 }, IsAllowedQuery: function (a, b) { return a.query && 0 < a.query.length && 0 < b.length }, OnFiledChange: function (a, b) { }, OnChooseSuggestion: function (a) { } }, jField: "string" == typeof a && "#" !== a[0] && "." !== a[0] ? $("#" + a) : $(a), SuggestionsClass: "u-AhunterSuggestions", SuggestionClass: "u-AhunterSuggestion",
            SelectedSuggestionClass: "u-AhunterSelectedSuggestion", EmptySuggestionClass: "u-AhunterEmptySuggestion", jSuggestions: $("<div></div>"), jAppendTo: "string" == typeof h && "#" !== h[0] && "." !== h[0] ? $("#" + h) : $(h), Suggestions: [], SelectedIndex: -1, UserValue: "", ShownValue: "", IsHidden: !0, GetValue: function () { return this.jField.val() }, SplitText: function (a, b) { for (var c = /[\s\\\/,;.\-"'<>\[\]()*]/gi, d = 0; d < a.length;) { var e = c.exec(a), e = null !== e ? e.index : a.length; d < e && b(d, e); d = e + 1 } }, FormatSuggestionValue: function (a) {
                var b = this.UserValue,
                c = []; this.SplitText(b, function (a, m) { c.push({ word: b.substring(a, m).toLowerCase(), prefix: m == b.length }) }); var d = "", e = 0; this.SplitText(a, function (b, l) { d += a.substring(e, b); var g = a.substring(b, l).toLowerCase(), h = f.Find(c, function (a, b) { return a.word == g || a.prefix && a.word == g.substring(0, a.word.length) }); h ? (d += "<strong>" + a.substring(b, b + h.word.length) + "</strong>", d += a.substring(b + h.word.length, l)) : d += a.substring(b, l); e = l }); d += a.substring(e); d.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g,
                    "&quot;").replace(/&lt;(\/?strong)&gt;/g, "<$1>"); return d
            }, ShowSuggestions: function () {
                var a = !0; this.UpdatePosition(); this.jSuggestions.empty(); this.Suggestions || (this.Suggestions = []); if (0 < this.Suggestions.length) for (var b = 0; b < this.Suggestions.length; b++)$("<div>" + this.FormatSuggestionValue(this.Suggestions[b].value) + "</div>").appendTo(this.jSuggestions).addClass(this.SuggestionClass); else g ? $("<div>" + g + "</div>").appendTo(this.jSuggestions).addClass(this.EmptySuggestionClass) : a = !1; a ? (this.jSuggestions.show(),
                    this.IsHidden = !1, this.HighliteSuggestion(this.SelectedIndex)) : this.ClearSuggestions()
            }, HideSuggestions: function () { this.jSuggestions.hide(); this.IsHidden = !0 }, ClearSuggestions: function () { this.Suggestions = []; this.SelectedIndex = -1; this.jSuggestions.empty(); this.HideSuggestions() }, UpdatePosition: function () {
                var a = this.jField.position(), b = this.jField.offsetParent().offset(), c = this.jAppendTo.offset(), d = a.left + (b.left - c.left), a = a.top + (b.top - c.top); this.jSuggestions.detach(); this.jSuggestions.appendTo(this.jAppendTo);
                this.jSuggestions.css("position", "absolute"); this.jSuggestions.css("left", d); this.jSuggestions.css("top", a + this.jField.outerHeight()); this.jSuggestions.css("width", this.jField.outerWidth())
            }, NavigateSuggestions: function (a) {
                var b = !1; a.which == e.Keys.ESC ? (b = !0, this.ResetField(this.UserValue), this.HideSuggestions()) : a.which == e.Keys.DOWN && 0 < this.Suggestions.length ? (b = !0, this.IsHidden ? (this.ShowSuggestions(), this.SelectSuggestion(this.SelectedIndex)) : this.SelectSuggestion(this.SelectedIndex + 1)) : a.which ==
                    e.Keys.UP && 0 < this.Suggestions.length ? (b = !0, this.IsHidden ? (this.ShowSuggestions(), this.SelectSuggestion(this.SelectedIndex)) : this.SelectSuggestion(this.SelectedIndex - 1)) : a.which == e.Keys.ENTER && !this.IsHidden && 0 <= this.SelectedIndex ? (b = !0, this.ResetField(this.ShownValue), this.EXT.OnChooseSuggestion.call(this, this.Suggestions[this.SelectedIndex]), this.ClearSuggestions()) : a.which != e.Keys.TAB || this.IsHidden || (0 <= this.SelectedIndex ? (this.ResetField(this.ShownValue), this.EXT.OnChooseSuggestion.call(this,
                        this.Suggestions[this.SelectedIndex])) : this.ResetField(this.UserValue), this.ClearSuggestions()); return !b
            }, SelectSuggestion: function (a) { this.SelectedIndex = -1 > a ? this.Suggestions.length - 1 : a >= this.Suggestions.length ? -1 : a; -1 != this.SelectedIndex ? (this.ShownValue = this.Suggestions[this.SelectedIndex].value, this.jField.val(this.ShownValue)) : this.ResetField(this.UserValue); this.HighliteSuggestion(this.SelectedIndex) }, ResetField: function (a, b) {
            this.UserValue === a || b || this.EXT.OnFiledChange.call(this, this.UserValue,
                a); this.ShownValue = this.UserValue = a; this.jField.val(a)
            }, HighliteSuggestion: function (a) { this.jSuggestions.children().removeClass(this.SelectedSuggestionClass); -1 != a && a < this.Suggestions.length && (a = this.jSuggestions.children().get(a), $(a).addClass(this.SelectedSuggestionClass)) }, UpdateSuggestions: function (a) {
                var b = this.jField.val(); if (this.ShownValue != b || a) {
                    var c = this; c.ResetField(b); c.SelectedIndex = -1; a = this.EXT.PrepareQuery.call(this, b); this.EXT.IsAllowedQuery.call(this, a, b) ? (b = $.extend({
                        output: "json",
                        ahungest: e.Version
                    }, a), $.ajax({ type: "GET", url: d, data: b, cache: !1, context: this, success: function (a) { c.Suggestions = a.suggestions; c.ShowSuggestions() } })) : c.ClearSuggestions()
                }
            }, Init: function () {
                var a = this; this.ResetField(this.jField.val(), !0); this.jSuggestions.addClass(this.SuggestionsClass); this.jSuggestions.css("max-height", c); this.jSuggestions.css("z-index", b); this.jSuggestions.hide(); this.UpdatePosition(); this.jField.attr("autocomplete", "off"); this.jSuggestions.on("mouseover" + e.EventsNamespace, "." +
                    this.SuggestionClass, function () { a.SelectedIndex = $(this).index(); a.HighliteSuggestion(a.SelectedIndex) }); this.jSuggestions.on("mouseout" + e.EventsNamespace, function () { a.SelectedIndex = -1; a.HighliteSuggestion(a.SelectedIndex) }); this.jSuggestions.on("mousedown" + e.EventsNamespace, "." + this.SuggestionClass, function (b) {
                        if (1 == b.which) return a.SelectedIndex = $(this).index(), a.SelectSuggestion(a.SelectedIndex), a.ResetField(a.ShownValue), a.EXT.OnChooseSuggestion.call(a, a.Suggestions[a.SelectedIndex]), a.ClearSuggestions(),
                            b = a.EXT.IsAllowedToSuggestOnFocus, a.EXT.IsAllowedToSuggestOnFocus = function () { return !1 }, a.jField.focus(), a.EXT.IsAllowedToSuggestOnFocus = b, !1
                    }); this.jField.on("keydown" + e.EventsNamespace, function (b) { return a.NavigateSuggestions(b) }); this.jField.on("keyup" + e.EventsNamespace, function () { a.UpdateSuggestions() }); this.jField.on("input" + e.EventsNamespace, function () { a.UpdateSuggestions() }); this.jField.on("change" + e.EventsNamespace, function () { a.UpdateSuggestions() }); this.jField.on("blur" + e.EventsNamespace,
                        function () { a.IsHidden || (a.ResetField(a.UserValue), a.ClearSuggestions()) }); this.jField.on("focus" + e.EventsNamespace, function () { a.EXT.IsAllowedToSuggestOnFocus.call(a) && a.UpdateSuggestions(!0) }); $(window).on("resize" + e.EventsNamespace, function () { a.UpdatePosition() })
            }, Dispose: function () { this.jSuggestions.off(e.EventsNamespace); this.jField.off(e.EventsNamespace); $(window).off(e.EventsNamespace); this.jSuggestions.remove() }
        }; a.Init(); return a
    } window.AhunterSuggest = {
        Address: {
            Discrete: function (a) {
                var c = {
                    obj: null,
                    deffered: !1
                }; $(document).ready(function () { c.obj = n(a) }); this.Running.push(c); return c
            }, Solid: function (a) { var c = { obj: null, deffered: !1 }; $(document).ready(function () { c.obj = q(a) }); this.Running.push(c); return c }, Running: []
        }, Person: { Discrete: function (a) { var c = { obj: null, deffered: !1 }; $(document).ready(function () { c.obj = r(a) }); this.Running.push(c) }, Solid: function (a) { var c = { obj: null, deffered: !1 }; $(document).ready(function () { c.obj = u(a) }); this.Running.push(c) }, Running: [] }, Dispose: function (a) {
            this.FindAndDispose(a,
                this.Address.Running); this.FindAndDispose(a, this.Person.Running); this.DisposeDeffered(this.Address.Running); this.DisposeDeffered(this.Person.Running)
        }, FindAndDispose: function (a, c) { for (var b = -1, d = 0; d < c.length; d++)c[d] === a && (b = d); -1 != b && (c[b].obj ? (c[b].obj.Dispose(), c.splice(b, 1)) : c[b].deffered = !0) }, DisposeDeffered: function (a) { for (var c = a.length - 1; 0 <= c; c--)a[c].deffered && a[c].obj && (a[c].obj.Dispose(), a.splice(c, 1)) }
    }; var e = {
        Version: "1.6", EventsNamespace: ".ahungest", AddressLevels: {
            Region: {
                name: "Region",
                sign: "r", num: 0
            }, District: { name: "District", sign: "d", num: 1 }, City: { name: "City", sign: "c", num: 2 }, Place: { name: "Place", sign: "p", num: 3 }, Site: { name: "Site", sign: "t", num: 4 }, Street: { name: "Street", sign: "s", num: 5 }, House: { name: "House", sign: "h", num: 6 }, Building: { name: "Building", sign: "b", num: 7 }, Structure: { name: "Structure", sign: "u", num: 8 }, Flat: { name: "Flat", sign: "f", num: 9 }
        }, ZipLevel: { name: "Zip", sign: "z" }, Keys: { ESC: 27, TAB: 9, ENTER: 13, UP: 38, DOWN: 40 }, DefaultOptions: {
            ahunter_url: "https://ahunter.ru/", empty_msg: "РќРµС‚ РїРѕРґС…РѕРґСЏС‰РµР№ РїРѕРґСЃРєР°Р·РєРё",
            limit: 10, suggest_on_empty: !1, suggest_on_focus: !0, z_index: 9999, max_height: 500, append_to: null, id: void 0, on_fetch: void 0, on_fetch_context: void 0, user: void 0, api_options: void 0
        }
    }, f = {
        Find: function (a, c) { for (var b = null, d = 0; d < a.length; d++)if (c(a[d])) { b = a[d]; break } return b }, IsLessLevel: function (a, c) { return e.AddressLevels[a].num < e.AddressLevels[c].num }, IsLessOrEqualLevel: function (a, c) { return e.AddressLevels[a].num <= e.AddressLevels[c].num }, IsNumericLevel: function (a) { return e.AddressLevels[a].num >= e.AddressLevels.House.num },
        IsAddressLevel: function (a) { return a in e.AddressLevels }, FindAddressLevelBySign: function (a) { var c = ""; for (Level in e.AddressLevels) if (e.AddressLevels[Level].sign == a) { c = e.AddressLevels[Level].name; break } return c }, GetLevelsSigns: function (a) { for (var c = "", b = 0; b < a.length; b++)c += e.AddressLevels[a[b]].sign; return c }, EscapeQuery: function (a) { return a.replace(/([\\\[\]])/g, "\\$1") }, DecodeMachineAddress: function (a) {
            for (var c = [], b = 0, d = "", g = { name: "", type: "", level: e.AddressLevels.Region.name }; b < a.length;) {
                if ("\\" !=
                    a[b] && ":" != a[b]) { var h = f.FindAddressLevelBySign(a[b]); "" === h ? d += a[b] : (g.name = d, c.push(g), d = "", g = { name: "", type: "", level: h }, g.level = h) } else "\\" == a[b] ? (b++ , b < a.length && (d += a[b])) : ":" == a[b] && (g.type = d, d = ""); b++
            } 0 < d.length && (g.name = d, c.push(g)); return c
        }, ExtendSuggester: function (a, c) { var b = a.EXT; $.extend(a, c); a.EXT = $.extend(b, c.EXT); return a }, SelectDefined: function () { for (var a, c = 0; c < arguments.length; c++)if (void 0 !== arguments[c]) { a = arguments[c]; break } return a }, BuildFieldOptions: function (a, c, b) {
            var d = {};
            for (Name in e.DefaultOptions) d[Name] = f.SelectDefined(a[Name], c[Name], e.DefaultOptions[Name]); "/" != d.ahunter_url[d.ahunter_url.length - 1] && (d.ahunter_url += "/"); d.append_to || (d.append_to = $(document.body)); d.full_url = d.ahunter_url + b; return d
        }
    }
})();