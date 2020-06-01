export class Advert {
    private _title: string;
    private _subtitle: string;
    private _description: string;
    private _photo: string;

    constructor(title: string, subtitle: string, description: string, photo: string) {
        this._photo = photo;
        this._title = title;
        this._subtitle = subtitle;
        this._description = description;
    }

    get title(): string {
        return this._title;
    }

    set title(value: string) {
        this._title = value;
    }

    get subtitle(): string {
        return this._subtitle;
    }

    set subtitle(value: string) {
        this._subtitle = value;
    }

    get description(): string {
        return this._description;
    }

    set description(value: string) {
        this._description = value;
    }

    get photo(): string {
        return this._photo;
    }

    set photo(value: string) {
        this._photo = value;
    }
}
