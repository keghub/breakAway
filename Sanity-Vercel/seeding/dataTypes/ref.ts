export interface IRef {
    _type: string;
    _ref: string;
}

export const createRef = (refType: string, refId: string): IRef => {
    return {
        _type: "reference",
        _ref: `${refType}-${refId}`,
    }
}
