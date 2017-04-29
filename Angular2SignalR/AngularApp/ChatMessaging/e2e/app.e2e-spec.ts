import { ChatMessagingPage } from './app.po';

describe('chat-messaging App', function() {
  let page: ChatMessagingPage;

  beforeEach(() => {
    page = new ChatMessagingPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
