class Orders {
    ref: Orders;
    instance: any;
    static route: string = "api/orders/";
    constructor(self: any) {
        this.instance = {
            defaultOptions: {},
            antiForgeryToken: "",
            start: model => self.invoke.call(
                self, Orders.route + "Start",
                "post", {}, model),
            removeFile: file => self.invoke.call(
                self, Orders.route + "edit/Consult/File",
                "post", {}, file),
            findOrders: model => self.invoke.call(
                self, Orders.route + "find/Orders",
                "post", {}, model),
            findConsults: model => self.invoke.call(
                self, Orders.route + "find/Consults",
                "post", {}, model),
            findConsultsByAccountID: model => self.invoke.call(
                self, Orders.route + "find/Consults/ByAccountID",
                "post", {}, model),
            getConsultDetails: id => self.invoke.call(
                self, Orders.route + "get/ConsultDetail/ByConsultationID/" + id,
                "get",{}),
            getConsultationsToBeAssigned: entity => self.invoke.call(
                self, Orders.route + "consultations/tobeassigned",
                "post", {}, entity),
            getConsultationsToBeServiced: entity => self.invoke.call(
                self, Orders.route + "consultations/tobeserviced",
                "post", {}, entity),
        }
    }
}
export = Orders;
