export class NewsItem {
    title: string;
    description: string;
    link: string;
    guid: string;
    pubDate: string;
    thumbnail: NewsThumbnail
}

export class NewsThumbnail{
    height: number;
    width: number;
    url: string
}